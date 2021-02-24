using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.InsuranceAdmin.Query.GetDokumentFürVermittler;
using Domain.Entities.Insurance;
using Domain.Enums;
using FluentAssertions;
using NUnit.Framework;

namespace Application.IntegrationTests.InsuranceAdmin.Queries.GetDokumentFürVermittler
{
    using static TestingFixture;
    
    public class GetDokumentFürVermittlerQueryTests : TestBase
    {
        [Test]
        public void AsAnonymous_ShouldReturnUnauthorizedAccessException()
        {
            var query = new GetDokumentFürVermittlerQuery();

            FluentActions.Invoking(() =>
                SendAsync(query)).Should().Throw<UnauthorizedAccessException>();
        }
        
        [Test]
        public void AsAdmin_UnexistingVermittler_ShouldReturnNotFoundException()
        {
            var user = RunAsAdminUser();
            
            var query = new GetDokumentFürVermittlerQuery()
            {
                Id = 1
            };

            user.IsAdmin.Should().BeTrue();
            FluentActions.Invoking(() =>
                SendAsync(query)).Should().Throw<NotFoundException>();
        }
        
        [Test]
        public async Task AsAdmin_UnexistingDokument_ShouldReturnNotFoundException()
        {
            var user = RunAsAdminUser();

            await CreateVermittlerMitDokument();
            
            var query = new GetDokumentFürVermittlerQuery()
            {
                Id = 1,
                DokumentId = 40
            };

            user.IsAdmin.Should().BeTrue();
            FluentActions.Invoking(() =>
                SendAsync(query)).Should().Throw<NotFoundException>();
        }
        
        [Test]
        public async Task AsAdmin_ShouldReturnVermittlerDokumentDto()
        {
            var user = RunAsAdminUser();

            await CreateVermittlerMitDokument();

            var query = new GetDokumentFürVermittlerQuery()
            {
                Id = 1,
                DokumentId = 1
            };

            var result = await SendAsync(query);

            user.IsAdmin.Should().BeTrue();
            result.GetType().Should().Be<VermittlerDokumentDto>();
            result.DokuemntenArtId.Should().Be(1);
            result.FileExtension.Should().Be(FileExtension.pdf.ToString());
        }
        
        [Test]
        public async Task AsBearbeiter_ShouldReturnVermittlerDokumentDto()
        {
            var user = RunAsBearbeiterUser();

            await CreateVermittlerMitDokument();

            var query = new GetDokumentFürVermittlerQuery()
            {
                Id = 1,
                DokumentId = 1
            };

            var result = await SendAsync(query);

            user.IsBearbeiter.Should().BeTrue();
            result.GetType().Should().Be<VermittlerDokumentDto>();
            result.DokuemntenArtId.Should().Be(1);
            result.FileExtension.Should().Be(FileExtension.pdf.ToString());
        }

        private async Task CreateVermittlerMitDokument()
        {
            var dokument = new Dokument()
            {
                Id = 1,
                Name = "Persönliche Daten",
                DokumentenArt = new DokumentArt
                {
                    Id = 1,
                    Name = "PersönlicheDaten"
                },
                Bearbeitungsstatus = Bearbeitungsstatus.ZuPrüfen,
                FileExtension = FileExtension.pdf,
                Data = Convert.FromBase64String("SGVsbG8gV29ybGQ=")
            };
            
            var dokumentListe = new List<Dokument>()
            {
                dokument
            };
            
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
                }, 
                RegistrierungsDokumente = dokumentListe
            });
        }
    }
}