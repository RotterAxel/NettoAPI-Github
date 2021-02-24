using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.InsuranceAdmin.Query.GetDokumentFürVermittler
{
    public class GetDokumentFürVermittlerQuery : IRequest<VermittlerDokumentDto>
    {
        public int Id { get; set; }
        public int DokumentId { get; set; }
    }
    
    public class GetDokumentFürVermittlerQueryHandler : IRequestHandler<GetDokumentFürVermittlerQuery, VermittlerDokumentDto>
    {
        private readonly IInsuranceDbContext _insuranceDbContext;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetDokumentFürVermittlerQueryHandler(IInsuranceDbContext insuranceDbContext,
            IMapper mapper,
            ICurrentUserService currentUserService)
        {
            _insuranceDbContext = insuranceDbContext;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<VermittlerDokumentDto> Handle(GetDokumentFürVermittlerQuery request, CancellationToken cancellationToken)
        {
            if (_currentUserService.IsAdmin || _currentUserService.IsBearbeiter)
            {
                if (!await _insuranceDbContext.Vermittler.AnyAsync(u => u.Id == request.Id, 
                    cancellationToken: cancellationToken))
                {
                    throw new NotFoundException($"Vermittler with Id {request.Id} does not exist.");
                }

                var dokumentFürVermittlerFromRepo = (await _insuranceDbContext.Vermittler
                    .Include(v => v.RegistrierungsDokumente)
                    .ThenInclude(rd => rd.DokumentenArt)
                    .FirstOrDefaultAsync(v => v.Id == request.Id))
                    .RegistrierungsDokumente.FirstOrDefault(d => d.Id == request.DokumentId);

                if (dokumentFürVermittlerFromRepo == null)
                {
                    throw new NotFoundException($"Dokument with Id {request.DokumentId} not found.");
                }

                return _mapper.Map<VermittlerDokumentDto>(dokumentFürVermittlerFromRepo);
            }
            throw new UnauthorizedAccessException();
        }
    }
}