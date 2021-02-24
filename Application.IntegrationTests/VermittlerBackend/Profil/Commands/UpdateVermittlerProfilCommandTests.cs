using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.VermittlerBackend.Profil.Commands;
using Domain.Entities.Insurance;
using Domain.Enums;
using FluentAssertions;
using FluentValidation;
using NUnit.Framework;

namespace Application.IntegrationTests.VermittlerBackend.Profil.Commands
{
    using static TestingFixture;
    
    public class UpdateVermittlerProfilCommandTests : TestBase
    {
        [Test]
        public void ShouldThrowValidationException()
        {
            UpdateVermittlerProfilCommand command = 
                new UpdateVermittlerProfilCommand();

            Func<Task> act = async () => { await SendAsync(command); };

            act.Should().Throw<ValidationException>()
                .Which.Errors.Count().Should().Be(13);
        }
        
        [Test]
        public async Task ShouldReturnUnauthorizedAccessException()
        {
            var command = await Create_UpdateVermittlerCommandAsync();

            FluentActions.Invoking(async () =>
                await SendAsync(command)).Should().Throw<UnauthorizedAccessException>();
        }
        
        [Test]
        public async Task AsNeuerVermittler_ShouldReturnBadRequestException()
        {
            RunAsNewVermittler();
            
            var command = await Create_UpdateVermittlerCommandAsync();

            FluentActions.Invoking(async () =>
                await SendAsync(command)).Should().Throw<BadRequestException>()
                .WithMessage("Vermittler existier nicht auf der API.");
        }
        
        private async Task<UpdateVermittlerProfilCommand> Create_UpdateVermittlerCommandAsync()
        {
            await AddAsync(new Land
            {
                Id = 1,
                Name = "Deutschland"
            });
            
            return new UpdateVermittlerProfilCommand()
            {
                Anrede = Anrede.Frau.ToString(),
                StaatsangehörigkeitId = 1,
                Telefon = "NeuesTelefon1234554",
                Geburtsort = "NeuerGeburtsort Cabo Verde",
                Geburtsdatum = new DateTime(1990, 4, 25),
                IhkRegistrierungsnummer = "NeueRegistrierungsnummer",
                Kontoinhaber = "NeuerKontoinhaber",
                Bankname = "NeuerBankname",
                Iban = "NeueIban",
                Bic = "NeueBic"
            };
        }
        
        [Test]
        public async Task AsNeuerVermittler_UnexistingLand_ShouldReturnNotFoundException()
        {
            RunAsVermittlerUser();
            
            var command = await Create_UpdateVermittlerCommandAsync();

            command.StaatsangehörigkeitId = -1;
            
            FluentActions.Invoking(async () =>
                    await SendAsync(command)).Should().Throw<NotFoundException>();
        }
        
        [Test]
        public async Task AsVermittler_ShouldUpdateVermittlerData()
        {
            await CreateVermittlerAsync();

            var vermittler = await FindVermittlerAsync(2);

            RunAsPassedInVermittler(vermittler);
            
            var command = 
                await Create_UpdateVermittlerCommandAsync();

            await SendAsync(command);

            var vermittlerAfterUpdate = await FindVermittlerAsync(2);

            vermittler.Id.Should().Be(2);
            vermittler.User.Nachname.Should().Be(vermittlerAfterUpdate.User.Nachname);
            vermittler.User.Vorname.Should().Be(vermittlerAfterUpdate.User.Vorname);
            vermittler.User.Adresse.Straße.Should().Be(vermittlerAfterUpdate.User.Adresse.Straße);
            vermittler.User.Adresse.Hausnummer.Should().Be(vermittlerAfterUpdate.User.Adresse.Hausnummer);
            vermittler.User.Adresse.Plz.Should().Be(vermittlerAfterUpdate.User.Adresse.Plz);
            vermittler.User.Adresse.Ort.Should().Be(vermittlerAfterUpdate.User.Adresse.Ort);

            vermittler.User.Anrede.Should().NotBe(vermittlerAfterUpdate.User.Anrede);            
            vermittler.User.Staatsangehörigkeit?.Should().NotBe(vermittlerAfterUpdate.User.Staatsangehörigkeit.Name);
            vermittler.User.Telefon.Should().NotBe(vermittlerAfterUpdate.User.Telefon);  
            vermittler.User.Geburtsort.Should().NotBe(vermittlerAfterUpdate.User.Geburtsort);
            vermittler.User.Geburtsdatum?.Should().NotBe(new DateTime(1990, 4, 25));
            vermittler.IhkRegistrierungsnummer.Should().NotBe(vermittlerAfterUpdate.IhkRegistrierungsnummer);  
            vermittler.Bankverbindung.Kontoinhaber.Should()
                .NotBeSameAs(vermittlerAfterUpdate.Bankverbindung.Kontoinhaber);  
            vermittler.Bankverbindung.BankName.Should().NotBe(vermittlerAfterUpdate.Bankverbindung.BankName);
            vermittler.Bankverbindung.IBAN.Should().NotBe(vermittlerAfterUpdate.Bankverbindung.IBAN);
            vermittler.Bankverbindung.BIC.Should().NotBe(vermittlerAfterUpdate.Bankverbindung.BIC);  
        }
        
        private async Task CreateVermittlerAsync()
        {
            await AddAsync(new Vermittler
            {
                Id = 2,
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
    }
}