using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.BackgroundTasks.Commands;
using Application.InsuranceAdmin.Commands.CreateGesellschaft;
using Application.VermittlerBackend.Profil.Queries.GetVermittlerGesellschaften;
using Domain.Entities.Insurance;
using Domain.Enums;
using FluentAssertions;
using NUnit.Framework;

namespace Application.IntegrationTests.VermittlerBackend.VermittlerGesellschaft.Queries
{
    using static TestingFixture;
    
    public class GetVermittlerGesellschaftenQueryTests : TestBase
    {
        [Test]
        public void AsAnonymous_ShouldReturnUnauthorizedAccessException()
        {
            var query = new GetVermittlerGesellschaftenQuery();

            FluentActions.Invoking(async () =>
                await SendAsync(query)).Should().Throw<UnauthorizedAccessException>();
        }
        
        [Test]
        public void AsAdmin_ShouldReturnUnauthorizedException()
        {
            RunAsAdminUser();

            var query = new GetVermittlerGesellschaftenQuery();

            FluentActions.Invoking(async () =>
                await SendAsync(query)).Should().Throw<UnauthorizedAccessException>();
        }
        
        [Test]
        public async Task AsRegisteredVermittler_ShouldReturnListVermittlerGesellschaftDto()
        {
            var vermittler = await CreateVermittler();

            var gesellschaft = await CreateGesellschaft();
            
            var vermittlerGesellschaft = 
                await CreateVemittlerGesellschaft(vermittler.Id, gesellschaft.Id);
            
            var user = RunAsPassedInVermittler(vermittler);
            
            var vermittlerGesellschaftListResult = await SendAsync(new GetVermittlerGesellschaftenQuery());

            user.IstVermittler.Should().Be(true);

            vermittlerGesellschaftListResult.GetType().Should().Be<List<VermittlerGesellschaftDto>>();
            vermittlerGesellschaftListResult.Count.Should().Be(1);
            vermittlerGesellschaftListResult[0].GetType().Should().Be<VermittlerGesellschaftDto>();
            vermittlerGesellschaftListResult[0].VermittlerId.Should().Be(vermittler.Id);
            vermittlerGesellschaftListResult[0].VermittlerNo.Should().Be(vermittlerGesellschaft.VermittlerNo);
            vermittlerGesellschaftListResult[0].GesellschaftName.Should().Be(gesellschaft.Name);
            vermittlerGesellschaftListResult[0].Abschlussvergütung.Should()
                .Be(vermittlerGesellschaft.Abschlussvergütung);
            vermittlerGesellschaftListResult[0].Bestandsvergütung.Should().Be(vermittlerGesellschaft.Bestandsvergütung);
            vermittlerGesellschaftListResult[0].MaxLaufzeitVergütung.Should()
                .Be(vermittlerGesellschaft.MaxLaufzeitVergütung);
        }

        private async Task<Gesellschaft> CreateGesellschaft()
        {
            var gesellschaft = new Gesellschaft()
            {
                Id = 1,
                Name = "Test"
            };
            
            await AddAsync(gesellschaft);

            return gesellschaft;
        }

        private async Task<VermittlerGesellschafft> CreateVemittlerGesellschaft(int vermittlerId, int gesellschaftId)
        {
            var vermittlerGesellschaft = new VermittlerGesellschafft()
            {
                VermittlerId = vermittlerId,
                GesellschaftId = gesellschaftId,
                VermittlerNo = "Testing",
                Abschlussvergütung = 0.08,
                Bestandsvergütung = 0.07,
                MaxLaufzeitVergütung = 40
            };
            
            await AddAsync(vermittlerGesellschaft);

            return vermittlerGesellschaft;
        }

        private async Task<Vermittler> CreateVermittler()
        {
            var vermittler = new Vermittler
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
                    Kontoinhaber = "TestKontoinhaber",
                    IBAN = "DE00000000000000000000",
                    BankName = "Bankname",
                    BIC = "DEUTDEDB123"
                },
                User = new User
                {
                    Id = 15,
                    KeycloakIdentifier = new Guid("106ee760-3e54-4fc9-a3b5-f6fc7284842f"),
                    EMail = "Vermittler@localhost",
                    Vorname = "Vermittler",
                    Nachname = "Markler",
                    Anrede = Anrede.Herr,
                    Geburtsdatum = new DateTime(1900, 1, 1),
                    Geburtsort = "TestOrt",
                    Staatsangehörigkeit = new Land()
                    {
                        Id=2,
                        Name = "Testland"
                    },
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
        
        
    }
}