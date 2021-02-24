using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Stammdaten.Queries.GetLänder;
using Domain.Entities.Insurance;
using FluentAssertions;
using NUnit.Framework;

namespace Application.IntegrationTests.Stammdaten.Queries.GetLänder
{
    using static TestingFixture;
    
    public class GetLänderTests : TestBase
    {
        [Test]
        public async Task AsAdmin_ShouldReturnListOfAllLänder()
        {
            var user = RunAsAdminUser();

            await CreateLänder();
            
            var result = await SendAsync(new GetLänderQuery());
        
            user.IsAdmin.Should().Be(true);
            result.Count.Should().Be(3);
            result.GetType().Should().Be<List<LandDto>>();
        }
        
        [Test]
        public async Task AsNewVermittler_ShouldReturnListOfAllLänder()
        {
            var user = RunAsNewVermittler();

            await CreateLänder();
            
            var result = await SendAsync(new GetLänderQuery());
        
            user.IstVermittler.Should().Be(true);
            result.Count.Should().Be(3);
            result.GetType().Should().Be<List<LandDto>>();
        }
        
        private async Task CreateLänder()
        {
            await AddAsync(new Land()
            {
                Id = 1,
                Name = "1"
            });
            
            await AddAsync(new Land()
            {
                Id = 2,
                Name = "2"
            });
            
            await AddAsync(new Land()
            {
                Id = 3,
                Name = "3"
            });
        }

    }
}