using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.InsuranceAdmin.Commands.UpdateVermittler;
using Domain.Entities.Insurance;
using Domain.Enums;
using FluentAssertions;
using NUnit.Framework;
using ValidationException = FluentValidation.ValidationException;

namespace Application.IntegrationTests.InsuranceAdmin.Commands.UpdateVermittler
{
    using static TestingFixture;
    
    public class UpdateVermittlerCommandTests : TestBase
    {
        [Test]
        public async Task UpdateVermittlerCommandAsAnonymous_ShouldReturnUnauthorizedAccessException()
        {
            await CreateVermittlerAsync();
            
            UpdateVermittlerCommand command = await CreateUpdateVermittlerCommandAsync();

            FluentActions.Invoking(async () =>
                await SendAsync(command)).Should().Throw<UnauthorizedAccessException>();
        }
        
        [Test]
        public async Task UpdateVermittlerCommandAsAdmin_ShouldUpdateUpdatedFields()
        {
            RunAsAdminUser();
            
            await CreateVermittlerAsync();
            
            UpdateVermittlerCommand command = await CreateUpdateVermittlerCommandAsync();

            await SendAsync(command);
            
            var updatedVermittler = await FindVermittlerAsync(1);

            updatedVermittler.User.Telefon.Should().NotBeNullOrEmpty();
            updatedVermittler.User.Anrede.Should().Be(Anrede.Herr);
        }
        
        [Test]
        public async Task UpdateVermittlerCommandAsBearbeiter_ShouldUpdateUpdatedFields()
        {
            RunAsBearbeiterUser();
            
            await CreateVermittlerAsync();
            
            UpdateVermittlerCommand command = await CreateUpdateVermittlerCommandAsync();

            await SendAsync(command);
            
            var updatedVermittler = await FindVermittlerAsync(1);

            updatedVermittler.User.Telefon.Should().NotBeNullOrEmpty();
            updatedVermittler.User.Anrede.Should().Be(Anrede.Herr);
        }
        
        [Test]
        public async Task UpdateVermittlerCommand_ShouldThrowNotFoundException()
        {
            RunAsBearbeiterUser();

            await CreateVermittlerAsync();

            UpdateVermittlerCommand command = await CreateUpdateVermittlerCommandAsync();

            command.Id = 4;
            
            FluentActions.Invoking(async () => 
                await SendAsync(command)).Should().Throw<NotFoundException>();
        }

        [Test]
        public void AsBearbeiter_ShouldThrowValidationException()
        {
            RunAsBearbeiterUser();

            UpdateVermittlerCommand command = CreateUpdateVermittlerCommandWithWrondValidations();

            Func<Task> act = async () => { await SendAsync(command); };

            act.Should().Throw<ValidationException>()
                .Which.Errors.Count().Should().Be(24);
        }
        
        private UpdateVermittlerCommand CreateUpdateVermittlerCommandWithWrondValidations()
        {
            return new UpdateVermittlerCommand
            {
                Id = 1,
                Vorname = null,
                Nachname = null,
                Anrede = "Wrong Anrede",
                Telefon = "12344341234",
                Fax = null,
                VermittlerRegistrierungsstatus = "Wrong Registrierungsstatus",
                BestandsProvisionssatz = -5.0f,
                AbschlussProvisionssatz = 1000.0f,
                IstAktiv = true,
                IBAN = null,
                Bankname = null,
                BIC = null,
                Straße = null,
                Hausnummer = null,
                Plz = null,
                Ort = null,
                Land = null
            };
        }
        
        private async Task<UpdateVermittlerCommand> CreateUpdateVermittlerCommandAsync()
        {
            var vermittler = await FindVermittlerAsync(1);
            
            return new UpdateVermittlerCommand
            {
                Id = 1,
                Vorname = vermittler.User.Vorname,
                Nachname = vermittler.User.Nachname,
                Anrede = Anrede.Herr.ToString(),
                Telefon = "12344341234",
                VermittlerRegistrierungsstatus = VermittlerRegistrierungsstatus.NeuerVermittler.ToString(),
                BestandsProvisionssatz = 60.0f,
                AbschlussProvisionssatz = 60.0f,
                IstAktiv = true,
                IBAN = vermittler.Bankverbindung.IBAN,
                Bankname = vermittler.Bankverbindung.BankName,
                BIC = vermittler.Bankverbindung.BIC,
                Straße = "VermittlerStraße",
                Hausnummer = "1",
                Plz = "123456",
                Ort = "Bremen",
                Land = "Deutschland"
            };
        }
        
        private async Task CreateVermittlerAsync()
        {
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
                }
            });
            
            await AddAsync(new Vermittler
            {
                Id = 2,
                VermittlerNo = "NP-000001",
                BestandsProvisionssatz = 60,
                IhkRegistrierungsnummer = "Registrierungsnummer",
                VermittlerRegistrierungsstatus = VermittlerRegistrierungsstatus.NeuerVermittler,
                User = new User()
                {
                    Id = 2,
                    KeycloakIdentifier = new Guid("e299be6e-dad4-4988-aac5-ee54fb581092"),
                    EMail = "Vermittler2@localhost",
                    Vorname = "Vermittler2",
                    Nachname = "Markler2",
                    Anrede = Anrede.Herr
                }
            });
        }
    }
}