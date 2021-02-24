using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities.Insurance;
using Domain.Enums;
using FluentAssertions;
using NUnit.Framework;

namespace Application.IntegrationTests.Common.Services
{
    using static TestingFixture;
    
    public class VermittlerNoGeneratorTests : TestBase
    {
        [Test]
        public async Task ShouldReturn6CharLongNoDifferentFromDbNumber()
        {
            var vermittlerNoList = await CreateVermittler();

            string vermittlerNo = await GenerateVermittlerNoAsync();

            vermittlerNo.Length.Should().Be(9);
            
            foreach (var vermittlerNoFromList in vermittlerNoList)
            {
                vermittlerNoFromList.Should().NotBeSameAs(vermittlerNo);
            }
        }

        private async Task<List<string>> CreateVermittler()
        {
            var vermittlerNoList = new List<string>()
            {
                "NP-123456",
                "NP-123457"
            };
            
            await AddAsync(new Vermittler
            {
                Id = 2,
                VermittlerRegistrierungsstatus = VermittlerRegistrierungsstatus.RegistrierungGenehmigt,
                VermittlerNo = vermittlerNoList[0],
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
            
            await AddAsync(new Vermittler
            {
                Id = 3,
                VermittlerRegistrierungsstatus = VermittlerRegistrierungsstatus.RegistrierungGenehmigt,
                VermittlerNo = vermittlerNoList[1],
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
                    Id = 3,
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
                            Id = 3,
                            Name = "Deutschland"
                        }
                    }
                }
            });
            
            return vermittlerNoList;
        }
    }
}