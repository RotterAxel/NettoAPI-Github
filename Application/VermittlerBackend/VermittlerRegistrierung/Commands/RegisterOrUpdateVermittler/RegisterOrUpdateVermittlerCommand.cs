using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Common;
using Domain.Entities.Insurance;
using Domain.Enums;
using Infrastructure.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.VermittlerBackend.VermittlerRegistrierung.Commands.RegisterOrUpdateVermittler
{
    public class RegisterOrUpdateVermittlerCommand : IRequest<int>
    {
        public string Anrede { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Telefon { get; set; }
        public int StaatsangehörigkeitId { get; set; }
        public string Geburtsort { get; set; }
        public DateTime Geburtsdatum { get; set; }
        public string IhkRegistrierungsnummer { get; set; }
        public string Straße { get; set; }
        public string Hausnummer { get; set; }
        public string Plz { get; set; }
        public string Ort { get; set; }
        public string Kontoinhaber { get; set; }
        public string Bankname { get; set; }
        public string Iban { get; set; }
        public string Bic { get; set; }
        public string EingeladenVonEinladeCode { get; set; }
    }

    public class RegisterOrUpdateVermittlerCommandHandler : IRequestHandler<RegisterOrUpdateVermittlerCommand, int>
    {
        private readonly IInsuranceDbContext _insuranceDbContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly IEinladecodeVermittlerValidation _einladecodeVermittlerValidation;
        private readonly IAESCryptographyService _iAesCryptographyService;
        private readonly IVermittlerNoGenerator _vermittlerNoGenerator;

        public RegisterOrUpdateVermittlerCommandHandler(
            IInsuranceDbContext insuranceDbContext,
            ICurrentUserService currentUserService,
            IEinladecodeVermittlerValidation einladecodeVermittlerValidation,
            IAESCryptographyService aesCryptographyService,
            IVermittlerNoGenerator vermittlerNoGenerator)
        {
            _insuranceDbContext = insuranceDbContext;
            _currentUserService = currentUserService;
            _einladecodeVermittlerValidation = einladecodeVermittlerValidation;
            _iAesCryptographyService = aesCryptographyService;
            _vermittlerNoGenerator = vermittlerNoGenerator;
        }

        public async Task<int> Handle(RegisterOrUpdateVermittlerCommand command, CancellationToken cancellationToken)
        {
            if (!_currentUserService.IstVermittler)
                throw new UnauthorizedAccessException();
            
            if (!await _insuranceDbContext.Länder.AnyAsync(l => l.Id == command.StaatsangehörigkeitId,
                cancellationToken))
                throw new NotFoundException(nameof(Land), command.StaatsangehörigkeitId);

            //If Vermittler has an ApiUserId already exists then Update
            //Else Register (Create)
            if (_currentUserService.ApiUserId != null)
                return await UpdateVermittler(command, cancellationToken);

            return await RegisterNewVermittler(command, cancellationToken);
        }

        /// <summary>
        /// Set Land in Adresse to Deutschland
        /// Set Provisionssätze to 60%
        /// Set Registrierungsstatus to NeuerVermittler
        /// Validate and Set EingeladenVon
        /// Create Einladecode for Vermittler
        /// Create new VermittlerNo
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task<int> RegisterNewVermittler(RegisterOrUpdateVermittlerCommand command, 
            CancellationToken cancellationToken)
        {
            var vermittlerToAdd = new Vermittler
            {
                BestandsProvisionssatz = 60.0f,
                AbschlussProvisionssatz = 60.0f,
                IhkRegistrierungsnummer = command.IhkRegistrierungsnummer,
                IstAktiv = false,
                VermittlerRegistrierungsstatus = VermittlerRegistrierungsstatus.NeuerVermittler,

                User = new User
                {
                    KeycloakIdentifier = new Guid(_currentUserService.KeycloakUserId),
                    EMail = _currentUserService.Email,
                    Vorname = command.Vorname,
                    Nachname = command.Nachname,
                    Telefon = command.Telefon,
                    Geburtsdatum = command.Geburtsdatum,
                    Geburtsort = command.Geburtsort,
                    StaatsangehörigkeitId = command.StaatsangehörigkeitId,
                    Anrede = EnumExtensions.ParseEnumFromString<Anrede>(command.Anrede),
                    Adresse = new Adresse
                    {
                        Straße = command.Straße,
                        Hausnummer = command.Hausnummer,
                        Plz = command.Plz,
                        Ort = command.Ort,
                        Land = await _insuranceDbContext.Länder.FirstAsync(l => l.Name == "Deutschland")
                    }
                },
                Bankverbindung = new Bankverbindung
                {
                    Kontoinhaber = command.Kontoinhaber,
                    IBAN = command.Iban,
                    BankName = command.Bankname,
                    BIC = command.Bic
                }
            };
            
            //START - EINGELADEN VON LOGIK
            if(!string.IsNullOrEmpty(command.EingeladenVonEinladeCode))
                await EingeladenVonZuweisen(command.EingeladenVonEinladeCode, vermittlerToAdd);
            //END
            
            //START - Add VermittlerNo
            vermittlerToAdd.VermittlerNo = await _vermittlerNoGenerator.GenerateVermittlerNoAsync();
            //END
            
            await _insuranceDbContext.Vermittler.AddAsync(vermittlerToAdd, cancellationToken);

            await _insuranceDbContext.SaveChangesAsync(cancellationToken);

            //For the vermittlerToAdd we need to Automatically generate a Einladungscode
            vermittlerToAdd.EinladecodeVermittler = new EinladecodeVermittler
            {
                VermittlerId = vermittlerToAdd.Id,
                Code = _iAesCryptographyService.EncryptString(vermittlerToAdd.Id.ToString())
            };
            
            //Add Vermittler to all Existing Gesellschaften
            await AddVermittlerToGesellschaften(vermittlerToAdd);

            await _insuranceDbContext.SaveChangesAsync(cancellationToken);

            return vermittlerToAdd.Id;
        }

        private async Task AddVermittlerToGesellschaften(Vermittler vermittlerToAdd)
        {
            var gesellschaftenListe = await _insuranceDbContext.GesellschaftSet.ToListAsync();

            List<VermittlerGesellschafft> vermittlerGesellschaften = new List<VermittlerGesellschafft>();
            
            foreach (var gesellschaft in gesellschaftenListe)
            {
                vermittlerGesellschaften.Add(new VermittlerGesellschafft
                {
                    VermittlerId = vermittlerToAdd.Id,
                    GesellschaftId = gesellschaft.Id,
                    VermittlerNo = vermittlerToAdd.VermittlerNo,
                    Abschlussvergütung = 0.08,
                    Bestandsvergütung = 0.08,
                    MaxLaufzeitVergütung = 40
                });
            }
            
            await _insuranceDbContext.VermittlerGesellschafften.AddRangeAsync(vermittlerGesellschaften);
        }

        private async Task EingeladenVonZuweisen(string eingeladenVonCode, Vermittler vermittlerToAdd)
        {
            //If the Vermittler was invited by another Vermittler
            //we search for the code in the DB
            var eingeladenVonEinladecode = await _insuranceDbContext.EinladecodesVermittler
                .FirstOrDefaultAsync(ev => ev.Code == eingeladenVonCode);

            //If the EingeladenVonCode exists in the DB
            //and it is Valid then we add the Code Id to the Newly created Vermittler
            if (eingeladenVonEinladecode != null)
            {
                if (_einladecodeVermittlerValidation.Validate(eingeladenVonCode))
                    vermittlerToAdd.EingeladenVonVermittlerEinladecodeId = eingeladenVonEinladecode.Id;
            }
        }

        private async Task<int> UpdateVermittler(RegisterOrUpdateVermittlerCommand command, 
            CancellationToken cancellationToken)
        {
            //Get Vermittler
            var vermittlerFromDb = await _insuranceDbContext.Vermittler
                .Include(v => v.User)
                .ThenInclude(u => u.Adresse)
                .ThenInclude(a => a.Land)
                .Include(v => v.Bankverbindung)
                .Include(v => v.RegistrierungsDokumente)
                .FirstAsync(v => v.UserId == _currentUserService.ApiUserId, cancellationToken);

            //Update Fields
            vermittlerFromDb.User.Anrede = EnumExtensions.ParseEnumFromString<Anrede>(command.Anrede);
            vermittlerFromDb.User.Vorname = command.Vorname;
            vermittlerFromDb.User.Nachname = command.Nachname;
            vermittlerFromDb.User.Telefon = command.Telefon;
            vermittlerFromDb.User.StaatsangehörigkeitId = command.StaatsangehörigkeitId;
            vermittlerFromDb.User.Geburtsort = command.Geburtsort;
            vermittlerFromDb.User.Geburtsdatum = command.Geburtsdatum;
            vermittlerFromDb.IhkRegistrierungsnummer = command.IhkRegistrierungsnummer;
            vermittlerFromDb.User.Adresse.Straße = command.Straße;
            vermittlerFromDb.User.Adresse.Hausnummer = command.Hausnummer;
            vermittlerFromDb.User.Adresse.Plz = command.Plz;
            vermittlerFromDb.User.Adresse.Ort = command.Ort;
            vermittlerFromDb.Bankverbindung.Kontoinhaber = command.Kontoinhaber;
            vermittlerFromDb.Bankverbindung.BankName = command.Bankname;
            vermittlerFromDb.Bankverbindung.IBAN = command.Iban;
            vermittlerFromDb.Bankverbindung.BIC = command.Bic;
            
            var eingeladenVonEinladecode = _insuranceDbContext.EinladecodesVermittler
                .FirstOrDefault(ev => ev.Code == command.EingeladenVonEinladeCode);

            if (eingeladenVonEinladecode != null)
            {
                if (_einladecodeVermittlerValidation.Validate(command.EingeladenVonEinladeCode))
                    vermittlerFromDb.EingeladenVonVermittlerEinladecodeId = eingeladenVonEinladecode.Id;
            }

            await _insuranceDbContext.SaveChangesAsync(cancellationToken);

            return vermittlerFromDb.Id;
        }
    }
}