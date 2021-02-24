using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.InsuranceAdmin.Commands.DeleteDokumentFürVermittler;
using Domain.Entities.Insurance;
using Domain.Enums;
using FluentAssertions;
using Infrastructure.Persistence.DbContexts.Insurance.InsuranceSeed;
using MediatR;
using NUnit.Framework;

namespace Application.IntegrationTests.InsuranceAdmin.Commands.SoftDeleteDokumentFürVermittler
{
    using static TestingFixture;
    
    public class SoftDeleteDokumentFürVermittlerCommandTests : TestBase
    {
        [Test]
        public async Task AsAnonymous_ShouldReturnUnauthorizedAccessException()
        {
            await CreateVermittlerAsync();
            
            SoftDeleteDokumentFürVermittlerCommand command = new SoftDeleteDokumentFürVermittlerCommand
            {
                VermittlerId = 1,
                DokumentId = 1
            };

            FluentActions.Invoking(async () =>
                await SendAsync(command)).Should().Throw<UnauthorizedAccessException>();
        }
        
        [Test]
        public async Task AsAdmin_ShouldSoftDeleteDokumentAndPutDokumentIntoHistory()
        {
            var user = RunAsAdminUser();

            await CreateVermittlerAsync();
            
            var command = new SoftDeleteDokumentFürVermittlerCommand
            {
                VermittlerId = 1,
                DokumentId = 1
            };

            var result = await SendAsync(command);
            var vermittler = await FindVermittlerAsync(1);

            user.IsAdmin.Should().BeTrue();
            result.Should().Be(Unit.Value);
            vermittler.RegistrierungsDokumente.Count().Should().Be(0);
            vermittler.RegistrierungsDokumenteHistorie.Count().Should().Be(1);
        }
        
        [Test]
        public async Task AsBearbeiter_ShouldSoftDeleteDokumentAndPutDokumentIntoHistory()
        {
            var user = RunAsBearbeiterUser();

            await CreateVermittlerAsync();
            
            var command = new SoftDeleteDokumentFürVermittlerCommand
            {
                VermittlerId = 1,
                DokumentId = 1
            };

            var result = await SendAsync(command);
            var vermittler = await FindVermittlerAsync(1);

            user.IsBearbeiter.Should().BeTrue();
            result.Should().Be(Unit.Value);
            vermittler.RegistrierungsDokumente.Count().Should().Be(0);
            vermittler.RegistrierungsDokumenteHistorie.Count().Should().Be(1);
        }
        
        [Test]
        public async Task AsAdmin_VermittlerNotExists_ShouldThrowNotFoundException()
        {
            var user = RunAsAdminUser();

            await CreateVermittlerAsync();
            
            var command = new SoftDeleteDokumentFürVermittlerCommand
            {
                VermittlerId = -1,
                DokumentId = 1
            };
            
            user.IsAdmin.Should().BeTrue();
            FluentActions.Invoking(async () =>
                await SendAsync(command)).Should().Throw<NotFoundException>()
                .WithMessage("Entity Vermittler (-1) was not found.");
        }
        
        [Test]
        public async Task AsAdmin_DokumentNotExists_ShouldThrowNotFoundException()
        {
            var user = RunAsAdminUser();

            await CreateVermittlerAsync();
            
            var command = new SoftDeleteDokumentFürVermittlerCommand
            {
                VermittlerId = 1,
                DokumentId = -1
            };

            user.IsAdmin.Should().BeTrue();
            FluentActions.Invoking(async () =>
                    await SendAsync(command)).Should().Throw<NotFoundException>()
                .WithMessage("Entity Dokument (-1) was not found.");
        }

        private async Task CreateVermittlerAsync()
        {
            List<Dokument> dokumentenListe = new List<Dokument>()
            {
                new Dokument()
                {
                    Id = 1,
                    Name = "Name",
                    DokumentenArt = new DokumentArt
                    {
                        Id = 1,
                        Name = "Ausweiskopie"
                    },
                    Bearbeitungsstatus = Bearbeitungsstatus.Aktzeptiert,
                    FileExtension = FileExtension.jpg,
                    Data = BeispielDokumente.Schufa
                }
            };
            
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
                }, 
                RegistrierungsDokumente = dokumentenListe
            });
        }
    }
}