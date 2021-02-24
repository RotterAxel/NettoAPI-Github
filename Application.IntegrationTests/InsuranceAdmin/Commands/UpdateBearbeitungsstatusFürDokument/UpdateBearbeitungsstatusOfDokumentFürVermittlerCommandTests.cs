using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.InsuranceAdmin.Commands.UpdateBearbeitungsstatusFürDokument;
using Domain.Entities.Insurance;
using Domain.Enums;
using FluentAssertions;
using Infrastructure.Persistence.DbContexts.Insurance.InsuranceSeed;
using NUnit.Framework;
using ValidationException = FluentValidation.ValidationException;

namespace Application.IntegrationTests.InsuranceAdmin.Commands.UpdateBearbeitungsstatusFürDokument
{
    using static TestingFixture;
    
    public class UpdateBearbeitungsstatusOfDokumentFürVermittlerCommandTests : TestBase
    {
        [Test]
        public async Task AsAnonymous_ShouldReturnUnauthorizedAccessException()
        {
            await CreateVermittlerAsync();
            
            var command = await Create_UpdateBearbeitungsstatusOfDokumentFürVermittlerCommandAsync();

            FluentActions.Invoking(async () =>
                await SendAsync(command)).Should().Throw<UnauthorizedAccessException>();
        }
        
        [Test]
        public async Task AsBearbeiter_ShouldThrowValidationException()
        {
            RunAsBearbeiterUser();

            await CreateVermittlerAsync();
            
            var command = 
                await CreateInvalidUpdateBearbeitungsstatusOfDokumentFürVermittlerCommandAsync();

            Func<Task> act = async () => { await SendAsync(command); };

            act.Should().Throw<ValidationException>()
                .Which.Errors.Count().Should().Be(1);
        }
        
        [Test]
        public async Task AsAdmin_ShouldUpdateDokument()
        {
            var user = RunAsAdminUser();

            await CreateVermittlerAsync();
            
            var command = 
                await Create_UpdateBearbeitungsstatusOfDokumentFürVermittlerCommandAsync();

            await SendAsync(command);

            var dokumentFromRepo = await FindAsync<Dokument>(1);

            user.IsAdmin.Should().BeTrue();
            dokumentFromRepo.Should().NotBeNull();
            dokumentFromRepo.Bearbeitungsstatus.Should()
                .Be(Bearbeitungsstatus.Aktzeptiert);
        }
        
        [Test]
        public async Task AsBearbeiter_ShouldUpdateDokument()
        {
            var user = RunAsBearbeiterUser();

            await CreateVermittlerAsync();
            
            var command = 
                await Create_UpdateBearbeitungsstatusOfDokumentFürVermittlerCommandAsync();

            await SendAsync(command);

            var dokumentFromRepo = await FindAsync<Dokument>(1);

            user.IsBearbeiter.Should().BeTrue();
            dokumentFromRepo.Should().NotBeNull();
            dokumentFromRepo.Bearbeitungsstatus.Should()
                .Be(Bearbeitungsstatus.Aktzeptiert);
        }
        
        [Test]
        public async Task AsAdmin_UnexistingVermittler_ShouldThrowNotFoundVermittler()
        {
            RunAsAdminUser();

            await CreateVermittlerAsync();
            
            var command = 
                await Create_UpdateBearbeitungsstatusOfDokumentFürVermittlerCommandAsync();

            command.VermittlerId = -1;

            FluentActions.Invoking(async () => 
                await SendAsync(command)).Should().Throw<NotFoundException>()
                .WithMessage("Entity Vermittler (-1) was not found.");
        }
        
        [Test]
        public async Task AsAdmin_UnexistingDokument_ShouldThrowNotFoundVermittler()
        {
            RunAsAdminUser();

            await CreateVermittlerAsync();
            
            var command = 
                await Create_UpdateBearbeitungsstatusOfDokumentFürVermittlerCommandAsync();

            command.DokumentId = -1;

            FluentActions.Invoking(async () => 
                await SendAsync(command)).Should().Throw<NotFoundException>()
                .WithMessage("Entity Dokument (-1) was not found.");
        }
        
        private async Task<UpdateBearbeitungsstatusOfDokumentFürVermittlerCommand> 
            Create_UpdateBearbeitungsstatusOfDokumentFürVermittlerCommandAsync()
        {
            var vermittler = await FindVermittlerAsync(1);

            return new UpdateBearbeitungsstatusOfDokumentFürVermittlerCommand()
            {
                VermittlerId = vermittler.Id,
                DokumentId = 1,
                Bearbeitungsstatus = Bearbeitungsstatus.Aktzeptiert.ToString()
            };
        }
        
        private async Task<UpdateBearbeitungsstatusOfDokumentFürVermittlerCommand> 
            CreateInvalidUpdateBearbeitungsstatusOfDokumentFürVermittlerCommandAsync()
        {
            var vermittler = await FindVermittlerAsync(1);
            
            return new UpdateBearbeitungsstatusOfDokumentFürVermittlerCommand()
            {
                VermittlerId = vermittler.Id,
                DokumentId = 1,
                Bearbeitungsstatus = "Falscher Bearbeitungsstatus"
            };
        }
        
        private async Task CreateVermittlerAsync()
        {
            List<Dokument> dokumentenListe = new List<Dokument>()
            {
                new Dokument()
                {
                    Id = 1,
                    Name = "Name",
                    DokumentenArt = new DokumentArt
                    {
                        Id = 1,
                        Name = "Ausweiskopie"
                    },
                    Bearbeitungsstatus = Bearbeitungsstatus.ZuPrüfen,
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