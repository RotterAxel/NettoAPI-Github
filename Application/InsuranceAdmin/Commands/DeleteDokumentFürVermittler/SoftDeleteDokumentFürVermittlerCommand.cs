using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities.Insurance;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.InsuranceAdmin.Commands.DeleteDokumentFürVermittler
{
    public class SoftDeleteDokumentFürVermittlerCommand : IRequest
    {
        public int DokumentId { get; set; }
        public int VermittlerId { get; set; }
    }

    public class SoftDeleteDokumentFürVermittlerCommandHandler : IRequestHandler<SoftDeleteDokumentFürVermittlerCommand>
    {
        private readonly IInsuranceDbContext _insuranceDbContext;
        private readonly ICurrentUserService _currentUserService;

        public SoftDeleteDokumentFürVermittlerCommandHandler(IInsuranceDbContext insuranceDbContext,
            ICurrentUserService currentUserService)
        {
            _insuranceDbContext = insuranceDbContext;
            _currentUserService = currentUserService;
        }
        
        public async Task<Unit> Handle(SoftDeleteDokumentFürVermittlerCommand command, CancellationToken cancellationToken)
        {
            if (_currentUserService.IsAdmin || _currentUserService.IsBearbeiter)
            {
                //Get Vermittler or null from the DB with Id == command.VermittlerId 
                var vermittler = await _insuranceDbContext.Vermittler
                    .Include(v => v.RegistrierungsDokumente)
                    //.Include(v => v.RegistrierungsDokumentenHistorie)
                    .ThenInclude(rd => rd.DokumentenArt)
                    .FirstOrDefaultAsync(v => v.Id == command.VermittlerId,
                        cancellationToken);
                
                //If Vermittler is null throw not found exception
                if (vermittler == null)
                    throw new NotFoundException(nameof(Vermittler), command.VermittlerId);
    
                //Get Dokument from Registrierungsdokument from Vermittler or null
                //from the DB with Id == command.DokumentId 
                var dokumentVonVermittlerToDelete =
                    vermittler.RegistrierungsDokumente.FirstOrDefault(d => d.Id == command.DokumentId);
                
                //If Dokument is null throw not found exception
                if (dokumentVonVermittlerToDelete == null)
                    throw new NotFoundException(nameof(Dokument), command.DokumentId);

                dokumentVonVermittlerToDelete.VermittlerRegistrierungsDokumentHistorienId = vermittler.Id;
                dokumentVonVermittlerToDelete.VermittlerRegistrierungsDokumentId = null;
                await _insuranceDbContext.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
            
            throw new UnauthorizedAccessException();
        }
    }
}