using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.InsuranceAdmin.Commands.CreateDokumentForVermittler;
using Domain.Entities.Insurance;
using Domain.Enums;
using Domain.Common;
using FluentAssertions;
using Infrastructure.Persistence.DbContexts.Insurance.InsuranceSeed;
using NUnit.Framework;
using ValidationException = FluentValidation.ValidationException;

namespace Application.IntegrationTests.InsuranceAdmin.Commands.CreateDokumentFürVermittler
{
    
    using static TestingFixture;
    
    public class CreateDokumentFürVermittlerTests : TestBase
    {
        [Test]
        public async Task AsAnonymous_ShouldReturnUnauthorizedAccessException()
        {
            await CreateVermittlerAsync();
            
            CreateDokumentFürVermittlerCommand command = await Create_CreateDokumentFürVermittlerCommandAsync();

            FluentActions.Invoking(async () =>
                await SendAsync(command)).Should().Throw<UnauthorizedAccessException>();
        }
        
        [Test]
        public async Task AsBearbeiter_ShouldThrowValidationException()
        {
            RunAsBearbeiterUser();

            await CreateVermittlerAsync();
            
            CreateDokumentFürVermittlerCommand command = 
                await CreateInvalidCreateDokumentFürVermittlerCommandAsync();

            Func<Task> act = async () => { await SendAsync(command); };

            act.Should().Throw<ValidationException>()
                .Which.Errors.Count().Should().Be(6);
        }
        
        [Test]
        public async Task AsAdmin_ShouldCreateDokumentAndReturnIdOfCorrectDokument()
        {
            RunAsAdminUser();

            await CreateVermittlerAsync();
            
            CreateDokumentFürVermittlerCommand command = 
                await Create_CreateDokumentFürVermittlerCommandAsync();

            var result = await SendAsync(command);

            var dokumentFromRepo = await FindAsync<Dokument>(result);

            dokumentFromRepo.Should().NotBeNull();
            dokumentFromRepo.Bearbeitungsstatus.Should()
                .Be(EnumExtensions.ParseEnumFromString<Bearbeitungsstatus>(command.Bearbeitungsstatus));
            dokumentFromRepo.Id.Should().Be(result);
        }
        
        [Test]
        public async Task AsVermittler_ShouldCreateDokumentAndSetBearbeitungsstatusZuPrüfen()
        {
            RunAsVermittlerUser();

            await CreateVermittlerAsync();
            
            CreateDokumentFürVermittlerCommand command = 
                await Create_CreateDokumentFürVermittlerCommandAsync();

            var result = await SendAsync(command);

            var dokumentFromRepo = await FindAsync<Dokument>(result);

            dokumentFromRepo.Should().NotBeNull();
            dokumentFromRepo.Bearbeitungsstatus.Should()
                .Be(Bearbeitungsstatus.ZuPrüfen);
        }
        
        [Test]
        public async Task AsAdmin_ShouldReturnNotFoundExceptionForUnknownDokumentArtId()
        {
            RunAsAdminUser();
        
            await CreateVermittlerAsync();
            
            CreateDokumentFürVermittlerCommand command = 
                await Create_CreateDokumentFürVermittlerCommandAsync();

            command.DokumentArtId = 5;
            
            FluentActions.Invoking(async () => 
                await SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public async Task AsAdmin_ShouldReturnNotFoundException()
        {
            RunAsAdminUser();

            await CreateVermittlerAsync();
            
            CreateDokumentFürVermittlerCommand command = 
                await Create_CreateDokumentFürVermittlerCommandAsync();

            command.VermittlerId = 500;
            
            FluentActions.Invoking(async () => 
                await SendAsync(command)).Should().Throw<NotFoundException>();
        }
        
        [Test]
        public async Task AsAdmin_ShouldReturnBadRequestExceptionVermittlerAlreadyHasDokumentWithSameDokumentArt()
        {
            RunAsAdminUser();

            await CreateVermittlerAsync();
            
            CreateDokumentFürVermittlerCommand command = 
                await Create_CreateDokumentFürVermittlerCommandAsync();

            command.DokumentArtId = 1;
            
            FluentActions.Invoking(async () => 
                await SendAsync(command)).Should().Throw<BadRequestException>();
        }
        
        private async Task<CreateDokumentFürVermittlerCommand> Create_CreateDokumentFürVermittlerCommandAsync()
        {
            var vermittler = await FindVermittlerAsync(1);

            await AddAsync(new DokumentArt
            {
                Id = 2,
                Name = "Neue Doku Art"
            });
            
            return new CreateDokumentFürVermittlerCommand
            {
                VermittlerId = vermittler.Id,
                Name = "ExampleDokument",
                DokumentArtId =  2,
                Bearbeitungsstatus = Bearbeitungsstatus.Aktzeptiert.ToString(),
                FileExtension = FileExtension.jpg.ToString(),
                Data = BeispielDokumente.Schufa
            };
        }
        
        private async Task<CreateDokumentFürVermittlerCommand> CreateInvalidCreateDokumentFürVermittlerCommandAsync()
        {
            var vermittler = await FindVermittlerAsync(1);
            
            return new CreateDokumentFürVermittlerCommand
            {
                VermittlerId = vermittler.Id,
                Name = null,
                DokumentArtId = 1,
                Bearbeitungsstatus = "Falscher Bearbeitungsstatus",
                FileExtension = "Falssche File Extension",
                Data = null
            };
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