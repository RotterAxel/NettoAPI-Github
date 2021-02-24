using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Stammdaten.Queries.GetDokumentArten;
using Domain.Entities.Insurance;
using FluentAssertions;
using NUnit.Framework;

namespace Application.IntegrationTests.Stammdaten.Queries.GetDokumentArten
{
    using static TestingFixture;
    
    public class GetDokumentArtenQueryTests : TestBase
    {
        [Test]
        public async Task AsAdmin_ShouldReturnIListDokumentArtÜbersichtDtoWith3DokumentArten()
        {
            var user = RunAsAdminUser();

            await CreateDokuemntArtenAsync();
            
            var result = await SendAsync(new GetDokumentArtenQuery());
        
            user.IsAdmin.Should().Be(true);
            result.Count.Should().Be(3);
            result.GetType().Should().Be<List<DokumentArtÜbersichtDto>>();
        }

        [Test]
        public async Task FürVermittler_ShouldReturn3Elements()
        {
            var user = RunAsBearbeiterUser();

            await CreateDokuemntArtenAsync();
            
            var result = await SendAsync(new GetDokumentArtenQuery());
        
            user.IsBearbeiter.Should().Be(true);
            result.Count.Should().Be(3);
            result.GetType().Should().Be<List<DokumentArtÜbersichtDto>>();        
        }
        
         private async Task CreateDokuemntArtenAsync()
        {
            await AddAsync(new DokumentArt()
            {
                Id = 1,
                Name = "1"
            });
            
            await AddAsync(new DokumentArt()
            {
                Id = 2,
                Name = "2"
            });
            
            await AddAsync(new DokumentArt()
            {
                Id = 3,
                Name = "3"
            });
        }
    }
}