using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.VermittlerBackend.VermittlerRegistrierung.Commands.CreateOrUpdateDokument;
using Domain.Entities.Insurance;
using Domain.Enums;
using FluentAssertions;
using Infrastructure.Persistence.DbContexts.Insurance.InsuranceSeed;
using NUnit.Framework;
using ValidationException = FluentValidation.ValidationException;

namespace Application.IntegrationTests.VermittlerBackend.VermittlerRegistrierung.Commands.CreateOrUpdateDokumentFürVemittler
{
    using static TestingFixture;
    
    public class CreateOrUpdateDokumentFürVemittlerCommandTests : TestBase
    {
        
        [Test]
        public async Task AsAnonymous_ShouldReturnUnauthorizedAccessException()
        {
            await CreateVermittlerAsync();
            
            CreateOrUpdateDokumentFürVermittlerCommand command = 
                await CreateDokumentFürVermittlerCommandAsync_NewDokument();

            FluentActions.Invoking(async () =>
                await SendAsync(command)).Should().Throw<UnauthorizedAccessException>();
        }
        
        [Test]
        public async Task AsNewVermittler_NotRegistered_ShouldReturnNotFoundException()
        {
            RunAsNewVermittler();
            
            await CreateVermittlerAsync();

            CreateOrUpdateDokumentFürVermittlerCommand command = 
                await CreateDokumentFürVermittlerCommandAsync_NewDokument();

            command.VermittlerId = -1;
            
            FluentActions.Invoking(async () =>
                await SendAsync(command)).Should().Throw<NotFoundException>();
        }
        
        [Test]
        public async Task ShouldThrowValidationException()
        {
            await CreateVermittlerAsync();
            
            CreateOrUpdateDokumentFürVermittlerCommand command = 
                await InvalidCreateOrUpdateDokumentFürVermittlerCommandAsync();

            Func<Task> act = async () => { await SendAsync(command); };

            act.Should().Throw<ValidationException>()
                .Which.Errors.Count().Should().Be(5);
        }
        
        [Test]
        public async Task AsNewVermittler_ShouldCreateDokumentAndSetBearbeitungsstatusZuPrüfen()
        {
            RunAsNewVermittler();
            
            await CreateNeuerVermittlerAsync();
            
            CreateOrUpdateDokumentFürVermittlerCommand command = 
                await CreateDokumentFürVermittlerCommandAsync_NewDokument();

            var result = await SendAsync(command);

            var dokumentFromRepo = await FindAsync<Dokument>(result);

            dokumentFromRepo.Should().NotBeNull();
            dokumentFromRepo.Bearbeitungsstatus.Should()
                .Be(Bearbeitungsstatus.ZuPrüfen);
            dokumentFromRepo.Name.Should().Be(command.Name);
        }
        
        [Test]
        public async Task AsNewVermittler_ShouldUpdateDokumentAndSetBearbeitungsstatusZuPrüfen()
        {
            RunAsNewVermittler();
            
            await CreateNeuerVermittlerAsync();
            
            CreateOrUpdateDokumentFürVermittlerCommand command = 
                await CreateDokumentFürVermittlerCommandAsync_ExistingDokument();

            var result = await SendAsync(command);

            var dokumentFromRepo = await FindAsync<Dokument>(result);

            dokumentFromRepo.Should().NotBeNull();
            dokumentFromRepo.Bearbeitungsstatus.Should()
                .Be(Bearbeitungsstatus.ZuPrüfen);
            dokumentFromRepo.Name.Should().Be(command.Name);
            dokumentFromRepo.FileExtension.Should().Be(FileExtension.png);
        }
        
        [Test]
        public async Task AsNewVermittler_ShouldReturnNotFoundExceptionForUnknownDokumentArtId()
        {
            RunAsNewVermittler();
        
            await CreateNeuerVermittlerAsync();
            
            var command = 
                await CreateDokumentFürVermittlerCommandAsync_ExistingDokument();

            command.DokumentArtId = 5;
            
            FluentActions.Invoking(async () => 
                await SendAsync(command)).Should().Throw<NotFoundException>();
        }
        
        private async Task<CreateOrUpdateDokumentFürVermittlerCommand> 
            CreateDokumentFürVermittlerCommandAsync_ExistingDokument()
        {
            var vermittler = await FindVermittlerAsync(1);

            return new CreateOrUpdateDokumentFürVermittlerCommand
            {
                VermittlerId = vermittler.Id,
                Name = "ExampleDokument2",
                DokumentArtId =  1,
                FileExtension = FileExtension.png.ToString(),
                Data = BeispielDokumente.Schufa
            };
        }
        
        private async Task<CreateOrUpdateDokumentFürVermittlerCommand> CreateDokumentFürVermittlerCommandAsync_NewDokument()
        {
            var vermittler = await FindVermittlerAsync(1);

            await AddAsync(new DokumentArt
            {
                Id = 2,
                Name = "Neue Doku Art"
            });
            
            return new CreateOrUpdateDokumentFürVermittlerCommand
            {
                VermittlerId = vermittler.Id,
                Name = "ExampleDokument",
                DokumentArtId =  2,
                FileExtension = FileExtension.jpg.ToString(),
                Data = BeispielDokumente.Schufa
            };
        }
        
        private async Task<CreateOrUpdateDokumentFürVermittlerCommand> 
            InvalidCreateOrUpdateDokumentFürVermittlerCommandAsync()
        {
            var vermittler = await FindVermittlerAsync(1);
            
            return new CreateOrUpdateDokumentFürVermittlerCommand
            {
                VermittlerId = vermittler.Id,
                Name = null,
                DokumentArtId = 1,
                FileExtension = "Falssche File Extension",
                Data = null
            };
        }
        
        private async Task CreateNeuerVermittlerAsync()
        {
            List<Dokument> dokumentenListe = new List<Dokument>()
            {
                new Dokument()
                {
                    Name = "Name",
                    DokumentenArt = new DokumentArt
                    {
                        Id = 1,
                        Name = "Ausweiskopie"
                    },
                    Bearbeitungsstatus = Bearbeitungsstatus.Aktzeptiert,
                    FileExtension = FileExtension.jpg,
                    Data = BeispielDokumente.Schufa
                }
            };
            
            await AddAsync(new Vermittler
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
                }, 
                RegistrierungsDokumente = dokumentenListe
            });
        }
        
        private async Task CreateVermittlerAsync()
        {
            List<Dokument> dokumentenListe = new List<Dokument>()
            {
                new Dokument()
                {
                    Name = "Name",
                    DokumentenArt = new DokumentArt
                    {
                        Id = 1,
                        Name = "Ausweiskopie"
                    },
                    Bearbeitungsstatus = Bearbeitungsstatus.Aktzeptiert,
                    FileExtension = FileExtension.jpg,
                    Data = BeispielDokumente.Schufa
                }
            };
            
            await AddAsync(new Vermittler
            {
                Id = 1,
                VermittlerNo = "NP-000000",
                VermittlerRegistrierungsstatus = VermittlerRegistrierungsstatus.RegistrierungGenehmigt,
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
                }, 
                RegistrierungsDokumente = dokumentenListe
            });
        }
    }
}