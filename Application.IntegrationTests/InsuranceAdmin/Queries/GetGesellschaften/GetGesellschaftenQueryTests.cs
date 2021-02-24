using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.InsuranceAdmin.Query.GetGesellschaften;
using Domain.Entities.Insurance;
using FluentAssertions;
using NUnit.Framework;

namespace Application.IntegrationTests.InsuranceAdmin.Queries.GetGesellschaften
{
    using static TestingFixture;
    
    public class GetGesellschaftenQueryTests : TestBase
    {
        [Test]
        public void AsAnonymous_ShouldReturnUnauthorizedAccessException()
        {
            var query = new GetGesellschaftenQuery();

            FluentActions.Invoking(() =>
                SendAsync(query)).Should().Throw<UnauthorizedAccessException>();
        }
        
        [Test]
        public async Task AsAdmin_ShouldReturnIListVermittlerÜbersichtDto()
        {
            var user = RunAsAdminUser();
        
            await CreateGesellschaften();
        
            var result = await SendAsync(new GetGesellschaftenQuery());
        
            user.IsAdmin.Should().Be(true);
            result.Count.Should().Be(2);
            result.GetType().Should().Be<List<GesellschaftÜbersichtDto>>();
            
            result[0].Id.Should().Be(1);
            result[0].Name.Should().Be("TestGesellschaft1");
            
            result[1].Id.Should().Be(2);
            result[1].Name.Should().Be("TestGesellschaft2");
        }

        private async Task CreateGesellschaften()
        {
            await AddAsync(new Gesellschaft
            {
                Id = 1,
                Name = "TestGesellschaft1"
            });
            
            await AddAsync(new Gesellschaft
            {
                Id = 2,
                Name = "TestGesellschaft2"
            });
        }
    }
}