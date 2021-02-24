using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.InsuranceAdmin.Query.GetVermittlerDetail;
using Domain.Entities.Insurance;
using Domain.Enums;
using FluentAssertions;
using Infrastructure.Persistence.DbContexts.Insurance.InsuranceSeed;
using NUnit.Framework;

namespace Application.IntegrationTests.InsuranceAdmin.Queries.GetVermittlerDetail
{
    using static TestingFixture;
    
    public class GetVermittlerDetailQueryTests : TestBase
    {
        [Test]
        public void AsAnonymous_ShouldReturnUnauthorizedAccessException()
        {
            var query = new GetVermittlerDetailQuery()
            {
                VermittlerId = 1
            };

            FluentActions.Invoking(async () =>
                await SendAsync(query)).Should().Throw<UnauthorizedAccessException>();
        }
        
        [Test]
        public void AsAdmin_ShouldReturnNotFoundException()
        {
            RunAsAdminUser();
        
            var query = new GetVermittlerDetailQuery()
            {
                VermittlerId = -1
            };

            FluentActions.Invoking(async () =>
                await SendAsync(query)).Should().Throw<NotFoundException>();
        }
        
        [Test]
        public async Task AsAdmin_ShouldReturnIListVermittlerÜbersichtDto()
        {
            var user = RunAsAdminUser();
            
            await CreateVermittler();

            var result = await SendAsync(new GetVermittlerDetailQuery()
            {
                VermittlerId = 2
            });

            user.IsAdmin.Should().Be(true);
            result.Anrede.Should().Be(Anrede.Herr.ToString());
            result.Id.Should().Be(2);
            result.Anrede.Should().Be(Anrede.Herr.ToString());
            result.Vorname.Should().Be("Vermittler");
            result.Nachname.Should().Be("Markler");
            result.Email.Should().Be("Vermittler@localhost");
            result.Telefon.Should().BeNullOrEmpty();
            result.StaatsangehörigkeitId.Should().Be(2);
            result.StaatsangehörigkeitName.Should().Be("Testland");
            result.Geburtsdatum.Should().Be(new DateTime(1900,1,1));
            result.Geburtsort.Should().Be("TestOrt");
            result.Fax.Should().BeNullOrEmpty();
            result.VermittlerRegistrierungsstatus.Should().Be(VermittlerRegistrierungsstatus.NeuerVermittler.ToString());
            result.BestandsProvisionssatz.Should().Be(60.0f);
            result.AbschlussProvisionssatz.Should().Be(60.0f);
            result.IhkRegistrierungsnummer.Should().Be("Registrierungsnummer");
            result.Kontoinhaber.Should().Be("TestKontoinhaber");
            result.IBAN.Should().Be("DE00000000000000000000");
            result.Bankname.Should().Be("Bankname");
            result.BIC.Should().Be("DEUTDEDB123");
            result.Straße.Should().Be("VermittlerStraße");
            result.Hausnummer.Should().Be("1");
            result.Plz.Should().Be("123456");
            result.Ort.Should().Be("Bremen");
            result.Hausnummer.Should().Be("1");
            result.Land.Should().Be("Deutschland");
            result.Einladecode.Should().Be("WgAA55grJGAGagrL2k0fsA==");
            result.GetType().Should().Be<VermittlerDetailansichtDto>();
            
            result.VermittlerRegistrierungsdokumenteUebersicht.GetType().Should()
                .Be<List<VermittlerVertragsdokumenteUebersichtDto>>();
            result.VermittlerRegistrierungsdokumenteUebersicht[0].DokuemntenArtId.Should().NotBe(0);
            result.VermittlerRegistrierungsdokumenteUebersicht[0].DokumentenArt.Should().NotBeNull();
            result.VermittlerRegistrierungsdokumenteUebersicht[0].Bearbeitungsstatus.Should()
                .Be(Bearbeitungsstatus.Aktzeptiert.ToString());
            result.VermittlerRegistrierungsdokumenteUebersicht[0].FileExtension.Should().Be(FileExtension.jpg.ToString());
            result.VermittlerRegistrierungsdokumenteUebersicht[0].Name.Should().Be("Name");
            result.VermittlerRegistrierungsdokumenteUebersicht[0].Id.Should().Be(1);
        }
        
        private async Task<Vermittler> CreateVermittler()
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
            
            
            
            var vermittler = new Vermittler
            {
                Id = 2,
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
                    Id = 1,
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
                }, 
                RegistrierungsDokumente = dokumentenListe,
                EinladecodeVermittler = new EinladecodeVermittler()
                {
                    VermittlerId = 1,
                    Code = "WgAA55grJGAGagrL2k0fsA=="
                }
            };

            await AddAsync(vermittler); 

            return vermittler;
        }
    }
}