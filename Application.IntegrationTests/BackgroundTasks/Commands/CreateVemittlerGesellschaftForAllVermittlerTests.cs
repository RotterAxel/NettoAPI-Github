using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.BackgroundTasks.Commands;
using Application.Common.Exceptions;
using Application.InsuranceAdmin.Commands.CreateGesellschaft;
using Domain.Entities.Insurance;
using Domain.Enums;
using FluentAssertions;
using NUnit.Framework;

namespace Application.IntegrationTests.BackgroundTasks.Commands
{
    using static TestingFixture;
    
    public class CreateVemittlerGesellschaftForAllVermittlerTests : TestBase
    {
        [Test]
        public async Task ShouldCreateVermittlerGesellschaftFürVermittler()
        {
            int gesellschaftId = await CreateGesellschaft();
            
            int vermittlerId = await CreateVemittler();

            var command = new CreateVemittlerGesellschaftForAllVermittler()
            {
                NeueGesellschaftsId = gesellschaftId
            };

            await SendAsync(command);

            var result = await FindVermittlerAsync(vermittlerId);

            result.VermittlerGesellschafften.Count.Should().BeGreaterThan(0);
            result.VermittlerGesellschafften[0].GesellschaftId.Should().Be(gesellschaftId);
            result.VermittlerGesellschafften[0].VermittlerId.Should().Be(vermittlerId);
        }
        
        [Test]
        public async Task ShouldThrowNotFoundException()
        {
            var command = new CreateVemittlerGesellschaftForAllVermittler()
            {
                NeueGesellschaftsId = -1
            };

            FluentActions.Invoking(async () =>
                await SendAsync(command)).Should().Throw<NotFoundException>();
        }
        
        [Test]
        public async Task ShouldCreateVermittlerGesellschaftFürVermittlerThatDoesNotAlreadyHaveThatGesellschaft()
        {
            int gesellschaftId = await CreateGesellschaft();
            
            int vermittlerId = await CreateVemittler();
            
            int vermittlerId2 = await CreateVemittler2MitGesellschaft(gesellschaftId);

            var command = new CreateVemittlerGesellschaftForAllVermittler()
            {
                NeueGesellschaftsId = gesellschaftId
            };

            //Vermittler2 should already have the VermittlerGesellschaft eintrag
            var resultVermittler2BeforeCommand = await FindVermittlerAsync(vermittlerId2);
            var vermittlerGesellschaftRowVersionForVermittler2 =
                resultVermittler2BeforeCommand.VermittlerGesellschafften[0].RowVersion;
            resultVermittler2BeforeCommand.VermittlerGesellschafften[0].GesellschaftId.Should().Be(gesellschaftId);
            resultVermittler2BeforeCommand.VermittlerGesellschafften[0].VermittlerId.Should().Be(vermittlerId2);
            
            await SendAsync(command);

            var result = await FindVermittlerAsync(vermittlerId);
            var resultVermittler2AfterCommand = await FindVermittlerAsync(vermittlerId2);

            result.VermittlerGesellschafften.Count.Should().BeGreaterThan(0);
            result.VermittlerGesellschafften[0].GesellschaftId.Should().Be(gesellschaftId);
            result.VermittlerGesellschafften[0].VermittlerId.Should().Be(vermittlerId);
            
            //Vermittler2 should still have the same VermittlerGesellschaft eintrag
            resultVermittler2AfterCommand.VermittlerGesellschafften[0].RowVersion.Should()
                .Be(vermittlerGesellschaftRowVersionForVermittler2);
        }

        private async Task<int> CreateVemittler()
        {
            int vermittlerIdToReturn = 1;

            await AddAsync(new Vermittler
            {
                Id = vermittlerIdToReturn,
                VermittlerNo = "NP-000000",
                BestandsProvisionssatz = 0,
                AbschlussProvisionssatz = 0,
                IstAktiv = false,
                IhkRegistrierungsnummer = "test",
                VermittlerRegistrierungsstatus = (VermittlerRegistrierungsstatus) 1,
                User = new User
                {
                    KeycloakIdentifier = new Guid(),
                    EMail = "test",
                    Vorname = "test",
                    Nachname = "test",
                    Telefon = "test",
                    Fax = "test",
                    Anrede = (Anrede) 1
                }
            });

            return vermittlerIdToReturn;
        }

        private async Task<int> CreateVemittler2MitGesellschaft(int gesellschaftId)
        {
            int vermittlerIdToReturn = 2;

            await AddAsync(new Vermittler
            {
                Id = vermittlerIdToReturn,
                VermittlerNo = "NP-000000",
                BestandsProvisionssatz = 0,
                AbschlussProvisionssatz = 0,
                IstAktiv = false,
                IhkRegistrierungsnummer = "test",
                VermittlerRegistrierungsstatus = (VermittlerRegistrierungsstatus) 1,
                User = new User
                {
                    KeycloakIdentifier = new Guid(),
                    EMail = "test",
                    Vorname = "test",
                    Nachname = "test",
                    Telefon = "test",
                    Fax = "test",
                    Anrede = (Anrede) 1
                },
                VermittlerGesellschafften = new List<VermittlerGesellschafft>()
                {
                    new VermittlerGesellschafft
                    {
                        VermittlerId = vermittlerIdToReturn,
                        GesellschaftId = gesellschaftId
                    }
                }
            });

            return vermittlerIdToReturn;
        }

        private async Task<int> CreateGesellschaft()
        {
            int gesellschaftIdToReturn = 1;

            await AddAsync(new Gesellschaft
            {
                Id = gesellschaftIdToReturn,
                Name = "TestGesellschaft"
            });

            return gesellschaftIdToReturn;
        }
    }
}