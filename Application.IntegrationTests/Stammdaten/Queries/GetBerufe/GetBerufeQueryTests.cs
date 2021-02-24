using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Stammdaten.Queries.GetBerufe;
using Domain.Entities.Insurance;
using FluentAssertions;
using NUnit.Framework;

namespace Application.IntegrationTests.Stammdaten.Queries.GetBerufe
{
    using static TestingFixture;
    
    public class GetBerufeQueryTests : TestBase
    {
        [Test]
        public async Task ShouldReturnListOfAllBerufe()
        {
            await CreateLänder();
            
            var result = await SendAsync(new GetBerufeQuery());
        
            result.Count.Should().Be(3);
            result.GetType().Should().Be<List<BerufDto>>();
        }
        
        private async Task CreateLänder()
        {
            await AddAsync(new Beruf()
            {
                Id = 1,
                Name = "1"
            });
            
            await AddAsync(new Beruf()
            {
                Id = 2,
                Name = "2"
            });
            
            await AddAsync(new Beruf()
            {
                Id = 3,
                Name = "3"
            });
        }
    }
}