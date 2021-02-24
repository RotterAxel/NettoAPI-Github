using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities.Insurance;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.InsuranceAdmin.Commands.UpdateBearbeitungsstatusFürDokument
{
    public class UpdateBearbeitungsstatusOfDokumentFürVermittlerCommand : IRequest
    {
        public int VermittlerId { get; set; }
        public int DokumentId { get; set; }
        public string Bearbeitungsstatus { get; set; }
    }

    public class
        UpdateBearbeitungsstatusOfDokumentFürVermittlerCommandHandler : IRequestHandler<
            UpdateBearbeitungsstatusOfDokumentFürVermittlerCommand>
    {
        private readonly IInsuranceDbContext _insuranceDbContext;
        private readonly ICurrentUserService _currentUserService;

        public UpdateBearbeitungsstatusOfDokumentFürVermittlerCommandHandler(
            IInsuranceDbContext insuranceDbContext,
            ICurrentUserService currentUserService)
        {
            _insuranceDbContext = insuranceDbContext;
            _currentUserService = currentUserService;
        }
        
        public async Task<Unit> Handle(UpdateBearbeitungsstatusOfDokumentFürVermittlerCommand command, 
            CancellationToken cancellationToken)
        {
            if (_currentUserService.IsAdmin || _currentUserService.IsBearbeiter)
            {
                //Get Vermittler or null from the DB with Id == command.VermittlerId 
                var vermittler = await _insuranceDbContext.Vermittler
                    .Include(v => v.RegistrierungsDokumente)
                    .ThenInclude(rd => rd.DokumentenArt)
                    .FirstOrDefaultAsync(v => v.Id == command.VermittlerId,
                        cancellationToken);
                
                //If Vermittler is null throw not found exception
                if (vermittler == null)
                    throw new NotFoundException(nameof(Vermittler), command.VermittlerId);
    
                //Get Dokument from Registrierungsdokument from Vermittler or null
                //from the DB with Id == command.DokumentId 
                var dokumentVonVermittlerToUpdate =
                    vermittler.RegistrierungsDokumente.FirstOrDefault(d => d.Id == command.DokumentId);
                
                //If Dokument is null throw not found exception
                if (dokumentVonVermittlerToUpdate == null)
                    throw new NotFoundException(nameof(Dokument), command.DokumentId);
                
                //Update the dokument
                dokumentVonVermittlerToUpdate.Bearbeitungsstatus =
                    EnumExtensions.ParseEnumFromString<Bearbeitungsstatus>(command.Bearbeitungsstatus);

                await _insuranceDbContext.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
            
            throw new UnauthorizedAccessException();
        }
    }
}