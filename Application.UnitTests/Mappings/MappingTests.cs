using System;
using Application.Common.Mappings;
using Application.InsuranceAdmin.Query.GetDokumentFürVermittler;
using Application.InsuranceAdmin.Query.GetGesellschaften;
using Application.InsuranceAdmin.Query.GetKunden;
using Application.InsuranceAdmin.Query.GetVermittler;
using Application.InsuranceAdmin.Query.GetVermittlerDetail;
using Application.Stammdaten.Queries.GetBerufe;
using Application.Stammdaten.Queries.GetDokumentArten;
using Application.Stammdaten.Queries.GetLänder;
using Application.Stammdaten.Queries.GetTitel;
using Application.VermittlerBackend.Profil.Queries.GetDokument;
using AutoMapper;
using Domain.Entities.Insurance;
using Xunit;

namespace Application.UnitTests.Mappings
{
    public class MappingTests
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingTests()
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = _configuration.CreateMapper();
        }

        [Fact]
        public void ShouldHaveValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }
        
        //AuditableEntity is an Abstract class, thats why its not included in the 
        //inline data.
        [Theory]
        [InlineData(typeof(Kunde), typeof(KundenÜbersichtDto))]
        [InlineData(typeof(User), typeof(UserKundenübersichtDto))]
        [InlineData(typeof(Vermittler), typeof(VermittlerÜbersichtDto))]
        [InlineData(typeof(Vermittler), typeof(VermittlerDetailansichtDto))]
        [InlineData(typeof(Dokument), typeof(VermittlerDokumentDto))]
        [InlineData(typeof(DokumentArt), typeof(DokumentArtÜbersichtDto))]
        [InlineData(typeof(Dokument), typeof(VermittlerVertragsdokumenteUebersichtDto))]
        [InlineData(typeof(Dokument), typeof(RegistrierungsDokumentDto))]
        [InlineData(typeof(Land), typeof(LandDto))]
        [InlineData(typeof(Beruf), typeof(BerufDto))]
        [InlineData(typeof(Titel), typeof(TitelDto))]
        [InlineData(typeof(Gesellschaft), typeof(GesellschaftÜbersichtDto))]
        public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
        {
            var instance = Activator.CreateInstance(source);
        
            _mapper.Map(instance, source, destination);
        }
    }
}