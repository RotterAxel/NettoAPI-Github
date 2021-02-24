using System;
using System.Threading.Tasks;
using Application.VermittlerBackend.Profil.Queries.GetVermittlerPolicy;
using Domain.Entities.Insurance;
using Domain.Enums;
using FluentAssertions;
using NUnit.Framework;

namespace Application.IntegrationTests.VermittlerBackend.Profil.Queries.GetVermittlerPolicy
{
    using static TestingFixture;

    public class GetVermittlerPolicyQueryTests : TestBase
    {
        [Test]
        public void AsAnonymous_ShouldReturnUnauthorizedAccessException()
        {
            var query = new GetVermittlerPolicyQuery();

            FluentActions.Invoking(() =>
                SendAsync(query)).Should().Throw<UnauthorizedAccessException>();
        }

        [Test]
        public async Task AsVermittler_ShouldReturnPolicyAllFalse()
        {
            var user = RunAsVermittlerUser();

            await CreateVermittlerAllFalsePolicy(user);

            var result = await SendAsync(new GetVermittlerPolicyQuery());

            user.IstVermittler.Should().Be(true);
            result.IstAktiv.Should().BeFalse();
            result.Registrierungsstatus.Should().Be(VermittlerRegistrierungsstatus.NeuerVermittler.ToString());
        }

        [Test]
        public async Task AsNewVermittler_ShouldReturnPolicyAllFalseAndNeuerVermittler()
        {
            var user = RunAsNewVermittler();

            var result = await SendAsync(new GetVermittlerPolicyQuery());

            user.IstVermittler.Should().Be(true);
            result.IstAktiv.Should().BeFalse();
            result.Registrierungsstatus.Should().Be(VermittlerRegistrierungsstatus.NeuerVermittler.ToString());
        }

        /// <summary>
        /// Create a Vermittler that is recently Registered to the site
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Vermittler that
        /// IstAktiv = false
        /// IstGenehimgt = false
        /// IstBevollmächtigter = false</returns>
        private async Task CreateVermittlerAllFalsePolicy(CurrentUser user)
        {
            await AddAsync(new Vermittler
            {
                Id = 1,
                VermittlerNo = "NP-000000",
                BestandsProvisionssatz = 60,
                AbschlussProvisionssatz = 60,
                IhkRegistrierungsnummer = "Registrierungsnummer",
                VermittlerRegistrierungsstatus = VermittlerRegistrierungsstatus.NeuerVermittler,
                User = new User
                {
                    Id = 1,
                    KeycloakIdentifier = new Guid(user.KeycloakUserGuid),
                    EMail = "Vermittler@localhost",
                    Vorname = "Vermittler",
                    Nachname = "Markler",
                    Anrede = Anrede.Herr
                }
            });
        }

        [Test]
        public async Task AsAdmin_ShouldReturnPolicyAllTrue()
        {
            var user = RunAsVermittlerUser();

            await CreateVermittlerAktivGenehmigt(user);

            var result = await SendAsync(new GetVermittlerPolicyQuery());

            user.IstVermittler.Should().Be(true);
            result.IstAktiv.Should().BeTrue();
            result.Registrierungsstatus.Should().Be(VermittlerRegistrierungsstatus.RegistrierungGenehmigt.ToString());
        }

        /// <summary>
        /// Create a Vermittler that is recently Registered to the site
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Vermittler that
        /// IstAktiv = false
        /// IstGenehimgt = false
        /// IstBevollmächtigter = false</returns>
        private async Task CreateVermittlerAktivGenehmigt(CurrentUser user)
        {
            await AddAsync(new Vermittler
            {
                Id = 1,
                VermittlerNo = "NP-000000",
                BestandsProvisionssatz = 60,
                AbschlussProvisionssatz = 60,
                IhkRegistrierungsnummer = "Registrierungsnummer",
                VermittlerRegistrierungsstatus = VermittlerRegistrierungsstatus.RegistrierungGenehmigt,
                IstAktiv = true,
                User = new User
                {
                    Id = 1,
                    KeycloakIdentifier = new Guid(user.KeycloakUserGuid),
                    EMail = "Vermittler@localhost",
                    Vorname = "Vermittler",
                    Nachname = "Markler",
                    Anrede = Anrede.Herr
                }
            });
        }
    }
}