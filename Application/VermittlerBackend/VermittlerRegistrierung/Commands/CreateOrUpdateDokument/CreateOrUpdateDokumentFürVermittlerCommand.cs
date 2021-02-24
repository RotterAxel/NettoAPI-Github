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

namespace Application.VermittlerBackend.VermittlerRegistrierung.Commands.CreateOrUpdateDokument
{
    public class CreateOrUpdateDokumentFürVermittlerCommand : IRequest<int>
    {
        public int VermittlerId { get; set; }
        public string Name { get; set; }
        public int DokumentArtId { get; set; }
        public string FileExtension { get; set; }
        public byte[] Data { get; set; }
    }

    public class CreateOrUpdateDokumentFürVermittlerCommandHandler :
        IRequestHandler<CreateOrUpdateDokumentFürVermittlerCommand, int>
    {
        private readonly IInsuranceDbContext _insuranceDbContext;
        private readonly ICurrentUserService _currentUserService;

        public CreateOrUpdateDokumentFürVermittlerCommandHandler(
            IInsuranceDbContext insuranceDbContext,
            ICurrentUserService currentUserService)
        {
            _insuranceDbContext = insuranceDbContext;
            _currentUserService = currentUserService;
        }
        
        public async Task<int> Handle(CreateOrUpdateDokumentFürVermittlerCommand command, CancellationToken cancellationToken)
        {
            if(!_currentUserService.IstVermittler)
                throw new UnauthorizedAccessException();
            
            //Check if passed in Dokumentart exist on the DokumentArt table. 
            //New Dokumentarten cannot be created in this method. 
            if(!await _insuranceDbContext
                .DokumentArtSet.AnyAsync(das => das.Id == command.DokumentArtId
                    , cancellationToken))
            {
                throw new NotFoundException($"Die Dokumentart mit der id {command.DokumentArtId} " +
                                            $"existiert nicht");
            }
            
            //Get Vermittler
            var vermittler = await _insuranceDbContext.Vermittler
                .Include(v => v.RegistrierungsDokumente)
                .ThenInclude(rd => rd.DokumentenArt)
                .FirstOrDefaultAsync(v => v.Id == command.VermittlerId,
                    cancellationToken);
                
            //Check if Requesting Vermittler exists on DB
            if (vermittler == null)
                throw new NotFoundException(nameof(Vermittler), command.VermittlerId);

            //If Vermittler has a Dokument with the same DokumentArt replace it
            if (vermittler.RegistrierungsDokumente
                .Any(rd => rd.DokumentenArt.Id == command.DokumentArtId))
            {
                var dokumentToReplace = vermittler.RegistrierungsDokumente
                    .First(rd => rd.DokumentenArt.Id == command.DokumentArtId);

                dokumentToReplace.Bearbeitungsstatus = Bearbeitungsstatus.ZuPrüfen;
                dokumentToReplace.Data = command.Data;
                dokumentToReplace.Name = command.Name;
                dokumentToReplace.FileExtension =
                    EnumExtensions.ParseEnumFromString<FileExtension>(command.FileExtension);

                await _insuranceDbContext.SaveChangesAsync(cancellationToken);
                
                return dokumentToReplace.Id;
            }
            
            //Dokument is new and is added to Registrierungsdokument for Vermittler
            Dokument dokumentToAdd = new Dokument
            {
                Name = command.Name,
                DokumentenArt = _insuranceDbContext.DokumentArtSet
                    .First(das => das.Id == command.DokumentArtId),
                Bearbeitungsstatus = Bearbeitungsstatus.ZuPrüfen,
                FileExtension = EnumExtensions.ParseEnumFromString<FileExtension>(command.FileExtension),
                Data = command.Data
            };
            
            vermittler.RegistrierungsDokumente.Add(dokumentToAdd);

            await _insuranceDbContext.SaveChangesAsync(cancellationToken);

            return dokumentToAdd.Id;
        }
    }
    
}