using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.InsuranceAdmin.Query.GetVermittler;
using Domain.Entities.Insurance;
using Domain.Enums;
using FluentAssertions;
using NUnit.Framework;

namespace Application.IntegrationTests.InsuranceAdmin.Queries.GetVermittler
{
    using static TestingFixture;
    
    public class GetVermittlerQueryTests : TestBase
    {
        [Test]
        public async Task AsAdmin_ShouldReturnIListVermittlerÜbersichtDto()
        {
            var user = RunAsAdminUser();
            
            await CreateVermittler();

            var result = await SendAsync(new GetVermittlerQuery());

            user.IsAdmin.Should().Be(true);
            result.Count.Should().Be(2);
            result.GetType().Should().Be<List<VermittlerÜbersichtDto>>();
            
        }
        
        private async Task CreateVermittler()
        {
            await AddAsync(new Vermittler
            {
                Id = 1,
                VermittlerNo = "NP-000000",
                BestandsProvisionssatz = 60,
                AbschlussProvisionssatz = 60,
                IhkRegistrierungsnummer = "Registrierungsnummer",
                VermittlerRegistrierungsstatus = VermittlerRegistrierungsstatus.RegistrierungGenehmigt,
                User = new User
                {
                    Id = 1,
                    KeycloakIdentifier = new Guid("106ee760-3e54-4fc9-a3b5-f6fc7284842f"),
                    EMail = "Vermittler@localhost",
                    Vorname = "Vermittler",
                    Nachname = "Markler",
                    Anrede = Anrede.Herr
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
        
        [Test]
        public async Task AsBearbeiter_ShouldReturnIListVermittlerÜbersichtDto()
        {
            var user = RunAsBearbeiterUser();
        
            await CreateVermittler();
        
            var result = await SendAsync(new GetVermittlerQuery());
        
            user.IsBearbeiter.Should().Be(true);
            result.Count.Should().Be(2);
            result.GetType().Should().Be<List<VermittlerÜbersichtDto>>();
            
        }
        
        [Test]
        public void AsAnonymous_ShouldReturnUnauthorizedAccessException()
        {
            var query = new GetVermittlerQuery();

            FluentActions.Invoking(() =>
                SendAsync(query)).Should().Throw<UnauthorizedAccessException>();
        }
    }
}