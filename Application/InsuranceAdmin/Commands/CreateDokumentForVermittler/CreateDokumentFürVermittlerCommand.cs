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

namespace Application.InsuranceAdmin.Commands.CreateDokumentForVermittler
{
    public class CreateDokumentFürVermittlerCommand : IRequest<int>
    {
        public int VermittlerId { get; set; }
        public string Name { get; set; }
        public int DokumentArtId { get; set; }
        public string Bearbeitungsstatus { get; set; }
        public string FileExtension { get; set; }
        public byte[] Data { get; set; }
    }

    public class CreateDokumentFürVermittlerCommandHandler : IRequestHandler<CreateDokumentFürVermittlerCommand, int>
    {
        private readonly IInsuranceDbContext _insuranceDbContext;
        private readonly ICurrentUserService _currentUserService;

        public CreateDokumentFürVermittlerCommandHandler(IInsuranceDbContext insuranceDbContext,
            ICurrentUserService currentUserService)
        {
            _insuranceDbContext = insuranceDbContext;
            _currentUserService = currentUserService;
        }

        public async Task<int> Handle(CreateDokumentFürVermittlerCommand command, CancellationToken cancellationToken)
        {
            if (_currentUserService.IsAdmin || _currentUserService.IsBearbeiter || _currentUserService.IstVermittler)
            {
                //Check if passed in Dokumentart exist on the DokumentArt table. 
                //New Dokumentarten cannot be created in this method. 
                if(!await _insuranceDbContext
                    .DokumentArtSet.AnyAsync(das => das.Id == command.DokumentArtId
                        , cancellationToken))
                {
                    throw new NotFoundException($"Die Dokumentart mit der id {command.DokumentArtId} " +
                                                $"existiert nicht");
                }
                
                //Get Vermittler or null from the DB with Id == command.VermittlerId 
                var vermittler = await _insuranceDbContext.Vermittler
                    .Include(v => v.RegistrierungsDokumente)
                    .ThenInclude(rd => rd.DokumentenArt)
                    .FirstOrDefaultAsync(v => v.Id == command.VermittlerId,
                        cancellationToken);
                
                //If Vermittler is null throw not found exception
                if (vermittler == null)
                    throw new NotFoundException(nameof(Vermittler), command.VermittlerId);

                //If Vermittler has a Dokument with the same DokumentArt throw a bad request exception.
                if (vermittler.RegistrierungsDokumente
                    .Any(rd => rd.DokumentenArt.Id == command.DokumentArtId))
                {
                    throw new BadRequestException($"Vermittler mit Id {command.VermittlerId} darf nicht zwei " +
                                                  $"Dokumente der Art {command.DokumentArtId} besitzen. Bitte löschen sie" +
                                                  " das Dokument mit der selben Dokument Art bevor sie ein " +
                                                  "neues hochladen");
                }
                
                //Put all command data in the dokument to add
                Dokument dokumentToAdd = new Dokument
                {
                    Name = command.Name,
                    DokumentenArt = _insuranceDbContext.DokumentArtSet
                        .First(das => das.Id == command.DokumentArtId),
                    Bearbeitungsstatus = EnumExtensions.ParseEnumFromString<Bearbeitungsstatus>(command.Bearbeitungsstatus),
                    FileExtension = EnumExtensions.ParseEnumFromString<FileExtension>(command.FileExtension),
                    Data = command.Data
                };
                
                //If the asking user is a vermittler set Bearbeitungsstatus to "Bearbeitungsstatus.ZuPrüfen"
                if (_currentUserService.IstVermittler)
                {
                    dokumentToAdd.Bearbeitungsstatus = Bearbeitungsstatus.ZuPrüfen;
                }

                vermittler.RegistrierungsDokumente.Add(dokumentToAdd);
                
                await _insuranceDbContext.SaveChangesAsync(cancellationToken);
                
                //Return Id of recently added dokument
                return dokumentToAdd.Id;
            }
            
            throw new UnauthorizedAccessException();
        }
    }
}