using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.VermittlerBackend.VermittlerRegistrierung.Commands.RegistrierungBeenden;
using Domain.Entities.Insurance;
using Domain.Enums;
using FluentAssertions;
using Infrastructure.Persistence.DbContexts.Insurance.InsuranceSeed;
using NUnit.Framework;

namespace Application.IntegrationTests.VermittlerBackend.VermittlerRegistrierung.Commands.RegistrierungBeenden
{
    using static TestingFixture;
    
    public class RegistrierungBeendenCommandTests : TestBase
    {
        [Test]
        public void AsAnonymous_ShouldReturnUnauthorizedAccessException()
        {
            var command = new RegistrierungBeendenCommand();

            FluentActions.Invoking(async () =>
                await SendAsync(command)).Should().Throw<UnauthorizedAccessException>();
        }
        
        [Test]
        public void AsKeycloakVermittler_ShouldReturnUnauthorizedAccessException()
        {
            RunAsNewVermittler();
            
            var command = new RegistrierungBeendenCommand();

            FluentActions.Invoking(async () =>
                await SendAsync(command)).Should().Throw<UnauthorizedAccessException>();
        }
        
        [Test]
        public async Task AsVermittlerWithoutRegistrierungsDoks_ShouldReturnBadRequestException()
        {
            var vermittler = await CreateVermittlerAsync();

            RunAsPassedInVermittler(vermittler);
            
            var command = new RegistrierungBeendenCommand();

            FluentActions.Invoking(async () =>
                await SendAsync(command)).Should().Throw<BadRequestException>()
                .WithMessage("Vermittler hat keine Registrierungsdokumente vorhanden");
        }
        
        private async Task<Vermittler> CreateVermittlerAsync()
        {
            Vermittler vermittler = new Vermittler
            {
                Id = 1,
                VermittlerNo = "NP-000000",
                VermittlerRegistrierungsstatus = VermittlerRegistrierungsstatus.NeuerVermittler,
                BestandsProvisionssatz = 60.0f,
                AbschlussProvisionssatz = 60.0f,
                IhkRegistrierungsnummer = "Registrierungsnummer",
                IstAktiv = true,
                Bankverbindung = new Bankverbindung
                {
                    Id = 1,
                    IBAN = "DE00000000000000000000",
                    BankName = "Bankname",
                    BIC = "DEUTDEDB123"
                },
                User = new User
                {
                    Id = 1,
                    KeycloakIdentifier = new Guid("106ee760-3e54-4fc9-a3b5-f6fc7284842f"),
                    EMail = "Vermittler@localhost",
                    Vorname = "Vermittler",
                    Nachname = "Markler",
                    Anrede = Anrede.Herr,
                    Adresse = new Adresse()
                    {
                        Straße = "VermittlerStraße",
                        Hausnummer = "1",
                        Plz = "123456",
                        Ort = "Bremen",
                        Land = new Land()
                        {
                            Name = "Deutschland"
                        }
                    }
                }
            };
            
            await AddAsync(vermittler);

            return vermittler;
        }
        
        [Test]
        public async Task AsVermittlerWithOneDokument_ShouldReturnBadRequestException()
        {
            var vermittler = await CreateVermittlerAsync();

            vermittler.RegistrierungsDokumente = CreateOneDokument();

            await UpdateAsync(vermittler);
            
            RunAsPassedInVermittler(vermittler);
            
            var command = new RegistrierungBeendenCommand();

            FluentActions.Invoking(async () =>
                await SendAsync(command)).Should().Throw<BadRequestException>();
        }

        private List<Dokument> CreateOneDokument()
        {
            return new List<Dokument>()
            {
                new Dokument()
                {
                    Name = "Name",
                    DokumentenArt = new DokumentArt
                    {
                        Name = "Ausweiskopie"
                    },
                    Bearbeitungsstatus = Bearbeitungsstatus.Aktzeptiert,
                    FileExtension = FileExtension.jpg,
                    Data = BeispielDokumente.Schufa
                }
            };
        }
        
        [Test]
        public async Task AsVermittlerWithAllDoks_NichtNeuerVermittler_ShouldReturnBadRequestException()
        {
            var vermittler = await CreateVermittlerAsync();

            vermittler.RegistrierungsDokumente = CreateAllErforderlicheDokument();
            vermittler.VermittlerRegistrierungsstatus = VermittlerRegistrierungsstatus.RegistrierungDurchgeführt;

            await UpdateAsync(vermittler);
            
            RunAsPassedInVermittler(vermittler);
            
            var command = new RegistrierungBeendenCommand();

            FluentActions.Invoking(async () =>
                await SendAsync(command)).Should().Throw<BadRequestException>()
                .WithMessage("Nur NeuerVermittler darf seine Registrierung beenden");
        }
        
        [Test]
        public async Task AsNeuerVermittlerWithAllDoks_ShouldReturnVermittlerId()
        {
            var vermittler = await CreateVermittlerAsync();

            vermittler.RegistrierungsDokumente = CreateAllErforderlicheDokument();

            await UpdateAsync(vermittler);
            
            RunAsPassedInVermittler(vermittler);
            
            var command = new RegistrierungBeendenCommand();

            var result = await SendAsync(command);

            result.Should().Be(1);
        }
        
        private List<Dokument> CreateAllErforderlicheDokument()
        {
            return new List<Dokument>()
            {
                new Dokument()
                {
                    Name = "Name",
                    DokumentenArt = new DokumentArt
                    {
                        Name = "34D-Nachweis"
                    },
                    Bearbeitungsstatus = Bearbeitungsstatus.Aktzeptiert,
                    FileExtension = FileExtension.jpg,
                    Data = BeispielDokumente.Schufa
                },
                new Dokument()
                {
                    Name = "Name",
                    DokumentenArt = new DokumentArt
                    {
                        Name = "Ausweiskopie"
                    },
                    Bearbeitungsstatus = Bearbeitungsstatus.Aktzeptiert,
                    FileExtension = FileExtension.jpg,
                    Data = BeispielDokumente.Schufa
                },
                new Dokument()
                {
                    Name = "Name",
                    DokumentenArt = new DokumentArt
                    {
                        Name = "Gewerbeanmeldung"
                    },
                    Bearbeitungsstatus = Bearbeitungsstatus.Aktzeptiert,
                    FileExtension = FileExtension.jpg,
                    Data = BeispielDokumente.Schufa
                }
            };
        }
    }
}