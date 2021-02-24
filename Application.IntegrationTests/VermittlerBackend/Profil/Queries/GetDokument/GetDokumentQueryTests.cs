using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.VermittlerBackend.Profil.Queries.GetDokument;
using Domain.Entities.Insurance;
using Domain.Enums;
using FluentAssertions;
using NUnit.Framework;

namespace Application.IntegrationTests.VermittlerBackend.Profil.Queries.GetDokument
{
    using static TestingFixture;
    
    public class GetDokumentQueryTests : TestBase
    {
        [Test]
        public void AsAnonymous_ShouldReturnUnauthorizedAccessException()
        {
            var query = new GetDokumentQuery();

            FluentActions.Invoking(() =>
                SendAsync(query)).Should().Throw<UnauthorizedAccessException>();
        }
        
        [Test]
        public async Task AsAdmin_UnexistingDokument_ShouldReturnNotFoundException()
        {
            var user = RunAsVermittlerUser();

            await CreateVermittlerMitDokument();
            
            var query = new GetDokumentQuery()
            {
                DokumentId = -1
            };

            user.IstVermittler.Should().BeTrue();
            
            FluentActions.Invoking(() =>
                SendAsync(query)).Should().Throw<NotFoundException>();
        }
        
        [Test]
        public async Task AsVermittler_ShouldReturnVermittlerDokumentDto()
        {
            var vermittler = await CreateVermittlerMitDokument();
            
            var user = RunAsPassedInVermittler(vermittler);
            
            var query = new GetDokumentQuery()
            {
                DokumentId = 1
            };

            var result = await SendAsync(query);

            user.IstVermittler.Should().BeTrue();
            result.GetType().Should().Be<RegistrierungsDokumentDto>();
            result.DokuemntenArtId.Should().Be(1);
            result.FileExtension.Should().Be(FileExtension.pdf.ToString());
        }
        
        private async Task<Vermittler> CreateVermittlerMitDokument()
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
            
            var vermittlerToAdd = new Vermittler
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
            };

            await AddAsync(vermittlerToAdd);
            return vermittlerToAdd;
        }
    }
}