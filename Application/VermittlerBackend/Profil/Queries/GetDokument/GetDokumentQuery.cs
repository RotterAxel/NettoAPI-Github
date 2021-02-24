using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.VermittlerBackend.Profil.Queries.GetDokument
{
    public class GetDokumentQuery : IRequest<RegistrierungsDokumentDto>
    {
        public int DokumentId { get; set; }
    }

    public class GetDokumentQueryHandler : IRequestHandler<GetDokumentQuery, RegistrierungsDokumentDto>
    {
        private readonly IInsuranceDbContext _insuranceDbContext;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetDokumentQueryHandler(
            IInsuranceDbContext insuranceDbContext,
            IMapper mapper,
            ICurrentUserService currentUserService)
        {
            _insuranceDbContext = insuranceDbContext;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }
        
        public async Task<RegistrierungsDokumentDto> Handle(GetDokumentQuery request, CancellationToken cancellationToken)
        {
            if (_currentUserService.IstVermittler && _currentUserService.ApiUserId != null)
            {
                var dokumentFromRepo = (await _insuranceDbContext.Vermittler
                        .Include(v => v.User)
                        .Include(v => v.RegistrierungsDokumente)
                        .ThenInclude(rd => rd.DokumentenArt)
                        .FirstOrDefaultAsync(v => v.User.Id == _currentUserService.ApiUserId))
                    .RegistrierungsDokumente.FirstOrDefault(d => d.Id == request.DokumentId);
                
                if (dokumentFromRepo == null)
                {
                    throw new NotFoundException($"Dokument with Id {request.DokumentId} not found.");
                }
            
                return _mapper.Map<RegistrierungsDokumentDto>(dokumentFromRepo);
            }
            throw new UnauthorizedAccessException();
        }
    }
}