using System;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.VermittlerBackend.VermittlerRegistrierung.Queries.GetEinladecodeValidität;
using Domain.Entities.Insurance;
using Domain.Enums;
using FluentAssertions;
using MediatR;
using NUnit.Framework;

namespace Application.IntegrationTests.VermittlerBackend.VermittlerRegistrierung.Queries.GetEinladecodeValidität
{
    using static TestingFixture;
    
    public class GetEinladecodeValiditätQueryTests : TestBase
    {
        [Test]
        public async Task ReturnOk()
        {
            var einladecode = await CreateEinladenderVermittlerAsync();

            GetEinladecodeValiditätQuery query = new GetEinladecodeValiditätQuery()
            {
                Code = einladecode.Code
            };

            Unit result = await SendAsync(query);

            result.Should().BeOfType<Unit>();
        }
        
        [Test]
        public async Task NotAESInteger_ShouldReturnBadRequest()
        {
            var einladecode = await CreateEinladenderVermittlerAsync();

            GetEinladecodeValiditätQuery query = new GetEinladecodeValiditätQuery()
            {
                Code = "yGeknLRHbugIBwracz5cgg=="
            };
            
            FluentActions.Invoking(async () =>
                    await SendAsync(query)).Should().Throw<BadRequestException>()
                .WithMessage("Einladecode ist invalide.");
        }
        
        [Test]
        public void EinladenderVermittlerExistiertNicht_NotFoundException()
        {
            GetEinladecodeValiditätQuery query = new GetEinladecodeValiditätQuery()
            {
                //Vermittler Id = 1 in diesem Code
                Code = "WgAA55grJGAGagrL2k0fsA=="
            };
            
            FluentActions.Invoking(async () =>
                    await SendAsync(query)).Should().Throw<NotFoundException>()
                .WithMessage("Vermittler des Einladecodes existiert nicht mehr auf unserer DB");
        }
        
        
        
        private async Task<EinladecodeVermittler> CreateEinladenderVermittlerAsync()
        {
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
                }, EinladecodeVermittler = new EinladecodeVermittler()
                {
                    VermittlerId = 1,
                    Code = "WgAA55grJGAGagrL2k0fsA=="
                }
            };
            
            await AddAsync(einladenderVermittler);

            return einladenderVermittler.EinladecodeVermittler;
        }
    }
}