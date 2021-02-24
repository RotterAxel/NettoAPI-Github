using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Application.VermittlerBackend.VermittlerRegistrierung.Queries.GetEinladecodeValidität
{
    public class GetEinladecodeValiditätQuery : IRequest
    {
        public string Code { get; set; }
    }

    public class GetEinladecodeValiditätQueryHandler : 
        IRequestHandler<GetEinladecodeValiditätQuery>
    {
        private readonly IInsuranceDbContext _insuranceDbContext;
        private readonly IAESCryptographyService _aesCryptographyService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<GetEinladecodeValiditätQuery> _logger;

        public GetEinladecodeValiditätQueryHandler(IInsuranceDbContext insuranceDbContext,
            IMapper mapper,
            IAESCryptographyService aesCryptographyService,
            IConfiguration configuration,
            ILogger<GetEinladecodeValiditätQuery> logger)
        {
            _insuranceDbContext = insuranceDbContext;
            _aesCryptographyService = aesCryptographyService;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<Unit> Handle(GetEinladecodeValiditätQuery request,
            CancellationToken cancellationToken)
        {
            int einladenderVermittlerId = -1;

            if (!string.IsNullOrEmpty(request.Code))
            {
                try
                {
                    var decryptedString = _aesCryptographyService.DecryptString(
                        request.Code);

                    if (!int.TryParse(decryptedString, out einladenderVermittlerId))
                        throw new BadRequestException("Einladecode ist invalide.");
                }
                catch (CryptographicException e)
                {
                    var requestName = nameof(GetEinladecodeValiditätQuery);
                    _logger.LogError(e,$"CryptographicException for Request {requestName} check AESKey is correct");
                    throw new InternalServerException("Server exception");
                }
            }
            
            if (einladenderVermittlerId == -1 ||
                string.IsNullOrEmpty(request.Code) ||
                !await _insuranceDbContext.Vermittler.AnyAsync(v => v.Id == einladenderVermittlerId))
            {
                throw new NotFoundException("Vermittler des Einladecodes existiert nicht mehr auf unserer DB");
            }
            
            return Unit.Value;
        }
    }
}