using System;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.InsuranceAdmin.Commands.UpdateGesellschaft;
using Domain.Entities.Insurance;
using FluentAssertions;
using FluentValidation;
using NUnit.Framework;

namespace Application.IntegrationTests.InsuranceAdmin.Commands.UpdateGesellschaft
{
    using static TestingFixture;
    
    public class UpdateGesellschaftCommandTests : TestBase
    {
        [Test]
        public async Task AsAnonymous_ShouldReturnUnauthorizedAccessException()
        {
            await CreateGesellschaftAsync();
            
            var command = new UpdateGesellschaftCommand()
            {
                Id = 1,
                Name = "Test"
            };

            FluentActions.Invoking(async () =>
                await SendAsync(command)).Should().Throw<UnauthorizedAccessException>();
        }

        private async Task CreateGesellschaftAsync()
        {
            await AddAsync(new Gesellschaft()
            {
                Id = 1,
                Name = "Test"
            });
        }

        [Test]
        public void AsAdmin_ShouldReturnValidationException()
        {
            RunAsAdminUser();
            
            var command = new UpdateGesellschaftCommand()
            {
                Name = null
            };
        
            FluentActions.Invoking(async () =>
                await SendAsync(command)).Should().Throw<ValidationException>();
        }
        
        [Test]
        public async Task AsAdmin_GesellschaftNotExists_ShouldReturnNotFoundException()
        {
            RunAsAdminUser();

            await CreateGesellschaftAsync();
            
            var command = new UpdateGesellschaftCommand()
            {
                Id = -1,
                Name = "Test1"
            };
        
            FluentActions.Invoking(async () =>
                await SendAsync(command)).Should().Throw<NotFoundException>();
        }
        
        [Test]
        public async Task AsAdmin_ShouldUpdateGesellschaft()
        {
            var user = RunAsAdminUser();

            await CreateGesellschaftAsync();
            
            var command = new UpdateGesellschaftCommand()
            {
                Id = 1,
                Name = "Test1"
            };
        
            await SendAsync(command);
        
            var gesellschaft = await FindAsync<Gesellschaft>(1);
        
            user.IsAdmin.Should().BeTrue();
            gesellschaft.Name.Should().Be("Test1");
        }
    }
}