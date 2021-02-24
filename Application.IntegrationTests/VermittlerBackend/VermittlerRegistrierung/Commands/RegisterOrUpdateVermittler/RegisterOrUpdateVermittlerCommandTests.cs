using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.VermittlerBackend.VermittlerRegistrierung.Commands.RegisterOrUpdateVermittler;
using Domain.Entities.Insurance;
using Domain.Enums;
using FluentAssertions;
using NUnit.Framework;
using ValidationException = FluentValidation.ValidationException;

namespace Application.IntegrationTests.VermittlerBackend.VermittlerRegistrierung.Commands.RegisterOrUpdateVermittler
{
    using static TestingFixture;
    
    public class RegisterOrUpdateVermittlerCommandTests : TestBase
    {
        [Test]
        public async Task ShouldReturnUnauthorizedAccessException()
        {
            RegisterOrUpdateVermittlerCommand command = await Create_RegisterOrUpdateVermittlerCommandAsync(null);

            FluentActions.Invoking(async () =>
                await SendAsync(command)).Should().Throw<UnauthorizedAccessException>();
        }
        
        [Test]
        public void ShouldThrowValidationException()
        {
            RegisterOrUpdateVermittlerCommand command = 
                new RegisterOrUpdateVermittlerCommand();

            Func<Task> act = async () => { await SendAsync(command); };

            act.Should().Throw<ValidationException>()
                .Which.Errors.Count().Should().Be(25);
        }
        
        [Test]
        public async Task AsVermittler_UnexistingLand_ReturnNotFoundException()
        {
            RunAsVermittlerUser();

            RegisterOrUpdateVermittlerCommand command = 
                await Create_RegisterOrUpdateVermittlerCommandAsync(null);

            command.StaatsangehörigkeitId = -1;
            
            FluentActions.Invoking(async () =>
                await SendAsync(command)).Should().Throw<NotFoundException>()
                .WithMessage("Entity Land (-1) was not found.");
        }
        
        [Test]
        public async Task AsVermittler_VermittlerAlreadyRegistered_ShouldUpdateVermittlerData()
        {
            await CreateVermittlerAsync();

            var vermittler = await FindVermittlerAsync(2);

            RunAsPassedInVermittler(vermittler);
            
            RegisterOrUpdateVermittlerCommand command = 
                await Create_RegisterOrUpdateVermittlerCommandAsync(null);

            var vermittlerIdAfterUpdate = await SendAsync(command);
            var vermittlerAfterUpdate = await FindVermittlerAsync(vermittlerIdAfterUpdate);

            vermittler.Id.Should().Be(vermittlerIdAfterUpdate);
            vermittler.User.Nachname.Should().NotBeSameAs(vermittlerAfterUpdate.User.Nachname);
            vermittler.User.Vorname.Should().NotBeSameAs(vermittlerAfterUpdate.User.Vorname);
            vermittler.IhkRegistrierungsnummer.Should().NotBeSameAs(vermittlerAfterUpdate.IhkRegistrierungsnummer);
            vermittlerAfterUpdate.EingeladenVonVermittlerEinladecodeId.Should().BeNull();
        }
        
        [Test]
        public async Task AsVermittler_EinladeCodeDoesNotPartseTo_ShouldUpdateVermittlerData()
        {
            await CreateVermittlerAsync();

            var vermittler = await FindVermittlerAsync(2);

            RunAsPassedInVermittler(vermittler);
            
            RegisterOrUpdateVermittlerCommand command = 
                await Create_RegisterOrUpdateVermittlerCommandAsync(null);

            var vermittlerIdAfterUpdate = await SendAsync(command);
            var vermittlerAfterUpdate = await FindVermittlerAsync(vermittlerIdAfterUpdate);

            vermittler.Id.Should().Be(vermittlerIdAfterUpdate);
            vermittler.User.Nachname.Should().NotBeSameAs(vermittlerAfterUpdate.User.Nachname);
            vermittler.User.Vorname.Should().NotBeSameAs(vermittlerAfterUpdate.User.Vorname);
            vermittler.IhkRegistrierungsnummer.Should().NotBeSameAs(vermittlerAfterUpdate.IhkRegistrierungsnummer);
            vermittlerAfterUpdate.EingeladenVonVermittlerEinladecodeId.Should().BeNull();
        }
        
        [Test]
        public async Task AsVermittler_VermittlerUpdateWithEinladeCode_ShouldUpdateVermittlerData()
        {
            var einladecodeVermittler = await CreateEinladenderVermittlerAsync();
            
            await CreateVermittlerAsync();

            var vermittler = await FindVermittlerAsync(1);

            RunAsPassedInVermittler(vermittler);
            
            RegisterOrUpdateVermittlerCommand command = 
                await Create_RegisterOrUpdateVermittlerCommandAsync(einladecodeVermittler.Code);

            var vermittlerIdAfterUpdate = await SendAsync(command);
            var vermittlerAfterUpdate = await FindVermittlerAsync(vermittlerIdAfterUpdate);

            vermittler.Id.Should().Be(vermittlerIdAfterUpdate);
            vermittler.User.Nachname.Should().NotBeSameAs(vermittlerAfterUpdate.User.Nachname);
            vermittler.User.Vorname.Should().NotBeSameAs(vermittlerAfterUpdate.User.Vorname);
            vermittler.IhkRegistrierungsnummer.Should().NotBeSameAs(vermittlerAfterUpdate.IhkRegistrierungsnummer);
            vermittlerAfterUpdate.EingeladenVonVermittlerEinladecodeId.Should().Be(5);
        }

        [Test]
        public async Task AsVermittler_NotIntAesMessage_ShouldReturnVermittlerWithEinnladecodeNull()
        {
            RunAsNewVermittler();
            
            RegisterOrUpdateVermittlerCommand command = 
                await Create_RegisterOrUpdateVermittlerCommandAsync("yGeknLRHbugIBwracz5cgg==");

            var result = await SendAsync(command);

            var vermittler = await FindVermittlerAsync(result);

            vermittler.EingeladenVonVermittlerEinladecodeId.Should().BeNull();
        }
        
        [Test]
        public async Task AsVermittler_VermittlerInEinladecodeNotExists_ShouldReturnVermittlerWithEinladecodeNull()
        {
            RunAsNewVermittler();
            
            RegisterOrUpdateVermittlerCommand command = 
                await Create_RegisterOrUpdateVermittlerCommandAsync("Pvm8SyluQacisQ28HIDr4Q==");
            
            var result = await SendAsync(command);

            var vermittler = await FindVermittlerAsync(result);

            vermittler.EingeladenVonVermittlerEinladecodeId.Should().BeNull();
            vermittler.EingeladenVon.Should().BeNull();
        }
        
        /// <summary>
        /// Should automatically set
        /// Land to Deutschland
        /// Provisionssatz to 60%
        /// Registrierungsstatus to NeuerVermittler
        /// Einladecode with his own Id
        /// Eingeladen von Vermittler einladecode 1
        /// </summary>
        /// <returns>Task</returns>
        [Test]
        public async Task AsVermittler_ShouldAddVermittlerToDbAndGenerateAutomaticValues()
        {
            RunAsNewVermittler();

            var einladecode = await CreateEinladenderVermittlerAsync();

            await CreateGesellschaften();
            
            RegisterOrUpdateVermittlerCommand command = 
                await Create_RegisterOrUpdateVermittlerCommandAsync(einladecode.Code);

            var result = await SendAsync(command);

            var vermittler = await FindVermittlerAsync(result);

            vermittler.User.Adresse.Land.Id.Should().Be(1);
            vermittler.AbschlussProvisionssatz.Should().Be(60.0f);
            vermittler.BestandsProvisionssatz.Should().Be(60.0f);
            vermittler.VermittlerRegistrierungsstatus.Should().Be(VermittlerRegistrierungsstatus.NeuerVermittler);
            vermittler.EinladecodeVermittler.Should().NotBeNull().And.NotBe("");
            vermittler.EinladecodeVermittler.VermittlerId.Should().Be(vermittler.Id);
            vermittler.EingeladenVonVermittlerEinladecodeId.Should().Be(5);
            vermittler.VermittlerGesellschafften.Count.Should().Be(2);
            
            foreach (var vermittlerGesellschafft in vermittler.VermittlerGesellschafften)
            {
                vermittlerGesellschafft.VermittlerId.Should().Be(vermittler.Id);
            }
        }

        private async Task CreateGesellschaften()
        {
            await AddAsync(new Gesellschaft
            {
                Name = "TestGesellschaft"
            });
            
            await AddAsync(new Gesellschaft
            {

                Name = "TestGesellschaft2"
            });
        }

        /// <summary>
        /// Should automatically set
        /// Land to Deutschland
        /// Provisionssatz to 60%
        /// Registrierungsstatus to NeuerVermittler
        /// Einladecode with his own Id
        /// Eingeladen von Vermittler 1
        /// </summary>
        /// <returns>Task</returns>
        [Test]
        public async Task AsVermittler_WithoutEinladecode_ShouldAddVermittlerToDbAndGenerateAutomaticValues()
        {
            RunAsNewVermittler();
            
            RegisterOrUpdateVermittlerCommand command = 
                await Create_RegisterOrUpdateVermittlerCommandAsync("null");

            var result = await SendAsync(command);

            var vermittler = await FindVermittlerAsync(result);

            vermittler.User.Adresse.Land.Id.Should().Be(1);
            vermittler.AbschlussProvisionssatz.Should().Be(60.0f);
            vermittler.BestandsProvisionssatz.Should().Be(60.0f);
            vermittler.VermittlerRegistrierungsstatus.Should().Be(VermittlerRegistrierungsstatus.NeuerVermittler);
            vermittler.EinladecodeVermittler.Should().NotBeNull().And.NotBe("");
            vermittler.EinladecodeVermittler.VermittlerId.Should().Be(vermittler.Id);
            vermittler.EingeladenVonVermittlerEinladecodeId.Should().BeNull();
        }
        
        private async Task CreateVermittlerAsync()
        {
            await AddAsync(new Vermittler
            {
                Id = 2,
                VermittlerRegistrierungsstatus = VermittlerRegistrierungsstatus.RegistrierungGenehmigt,
                VermittlerNo = "NP-1234",
                BestandsProvisionssatz = 60.0f,
                AbschlussProvisionssatz = 60.0f,
                IhkRegistrierungsnummer = "Registrierungsnummer",
                IstAktiv = true,
                Bankverbindung = new Bankverbindung
                {
                    IBAN = "DE00000000000000000000",
                    BankName = "Bankname",
                    BIC = "DEUTDEDB123"
                },
                User = new User
                {
                    Id = 2,
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
                            Id = 2,
                            Name = "Deutschland"
                        }
                    }
                }
            });
        }

        private async Task<EinladecodeVermittler> CreateEinladenderVermittlerAsync()
        {
            //Einladender Vermittler HAS to be 1 because of the einladecode
            var einladenderVermittler = new Vermittler()
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
                    }, EinladecodeVermittler = new EinladecodeVermittler()
                    {
                        Id = 5,
                        VermittlerId = 1,
                        Code = "WgAA55grJGAGagrL2k0fsA=="
                    }
                };
            
            await AddAsync(einladenderVermittler);

            return einladenderVermittler.EinladecodeVermittler;
        }
        
        private async Task<RegisterOrUpdateVermittlerCommand> Create_RegisterOrUpdateVermittlerCommandAsync(string einladecode)
        {
            await AddAsync(new Land
            {
                Id = 1,
                Name = "Deutschland"
            });
            
            return new RegisterOrUpdateVermittlerCommand
            {
                Anrede = Anrede.Frau.ToString(),
                Vorname = "Hildi",
                Nachname = "Hildegard",
                StaatsangehörigkeitId = 1,
                Telefon = "1234554",
                Geburtsort = "Cabo Verde",
                Geburtsdatum = new DateTime(1990, 4, 25),
                IhkRegistrierungsnummer = "TestRegistrierung",
                Straße = "Test Straße",
                Hausnummer = "44",
                Plz = "12345",
                Ort = "Homburg",
                Kontoinhaber = null,
                Bankname = "Bankname",
                Iban = "Iban",
                Bic = "Bic",
                EingeladenVonEinladeCode = einladecode
            };
        }
    }
}