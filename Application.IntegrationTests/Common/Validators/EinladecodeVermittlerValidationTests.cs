using System;
using System.Threading.Tasks;
using Domain.Entities.Insurance;
using Domain.Enums;
using FluentAssertions;
using NUnit.Framework;

namespace Application.IntegrationTests.Common.Validators
{
    using static TestingFixture;
    
    public class EinladecodeVermittlerValidationTests : TestBase
    {
        [Test]
        public void EinladecodeIsNotIntParseable_ShouldReturnFalse()
        {
            string message = "Test message";
            
            ValidateEinladecodeVermittler(AESEncrypt(message)).Should().BeFalse();
        }
        
        [Test]
        public void EinladecodeIsNull_ShouldReturnFalse()
        {
            ValidateEinladecodeVermittler(null).Should().BeFalse();
        }
        
        [Test]
        public void EinladecodeIsEmpty_ShouldReturnFalse()
        {
            ValidateEinladecodeVermittler("").Should().BeFalse();
        }
        
        [Test]
        public async Task EinladecodeIsValid_EinladenderVermittlerExists_ShouldReturnTrue()
        {
            ValidateEinladecodeVermittler((await CreateEinladenderVermittlerAsync()).Code)
                .Should().BeTrue();
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
                        Code = AESEncrypt("1")
                    }
                };
            
            await AddAsync(einladenderVermittler);

            return einladenderVermittler.EinladecodeVermittler;
        }
    }
}