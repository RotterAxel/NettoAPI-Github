using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Stammdaten.Queries.GetTitel;
using Domain.Entities.Insurance;
using FluentAssertions;
using NUnit.Framework;

namespace Application.IntegrationTests.Stammdaten.Queries.GetTitel
{
    using static TestingFixture;
    
    public class GetTitelQueryTests : TestBase
    {
        [Test]
        public async Task ShouldReturnListOfAllTitel()
        {
            var user = RunAsAdminUser();

            await CreateTitel();
            
            var result = await SendAsync(new GetTitelQuery());
        
            user.IsAdmin.Should().Be(true);
            result.Count.Should().Be(3);
            result.GetType().Should().Be<List<TitelDto>>();
        }
        
        private async Task CreateTitel()
        {
            await AddAsync(new Titel()
            {
                Id = 1,
                BezeichnungKurz = "1",
                Beschreibung = "12312412"
            });
            
            await AddAsync(new Titel()
            {
                Id = 2,
                BezeichnungKurz = "2",
                Beschreibung = "12312412"
            });
            
            await AddAsync(new Titel()
            {
                Id = 3,
                BezeichnungKurz = "3",
                Beschreibung = "12312412"
            });
        }
    }
}