using System;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.InsuranceAdmin.Commands.CreateGesellschaft;
using Domain.Entities.Insurance;
using FluentAssertions;
using FluentValidation;
using NUnit.Framework;

namespace Application.IntegrationTests.InsuranceAdmin.Commands.CreateGesellschaft
{
    using static TestingFixture;

    public class CreateGesellschaftCommandTests : TestBase
    {
        [Test]
        public void AsAnonymous_ShouldReturnUnauthorizedAccessException()
        {
            var command = new CreateGesellschaftCommand()
            {
                Name = "Test"
            };

            FluentActions.Invoking(async () =>
                await SendAsync(command)).Should().Throw<UnauthorizedAccessException>();
        }
        
        [Test]
        public void AsAdmin_ShouldReturnValidationException()
        {
            RunAsAdminUser();
            
            var command = new CreateGesellschaftCommand()
            {
                Name = null
            };

            FluentActions.Invoking(async () =>
                await SendAsync(command)).Should().Throw<ValidationException>();
        }
        
        [Test]
        public async Task AsAdmin_ShouldReturnBadRequestException()
        {
            RunAsAdminUser();

            await AddAsync(new Gesellschaft()
            {
                Name = "Test1"
            });
            
            var command = new CreateGesellschaftCommand()
            {
                Name = "Test1"
            };

            FluentActions.Invoking(async () =>
                await SendAsync(command)).Should().Throw<BadRequestException>();
        }
        
        [Test]
        public async Task AsAdmin_ShouldCreateGesellschaftAndReturnInt()
        {
            var user = RunAsAdminUser();
            
            var command = new CreateGesellschaftCommand()
            {
                Name = "Test"
            };

            var result = await SendAsync(command);

            var gesellschaft = await FindAsync<Gesellschaft>(result);

            user.IsAdmin.Should().BeTrue();
            gesellschaft.Name.Should().Be("Test");
            gesellschaft.VermittlerGesellschafften.Count.Should().Be(0);
        }
    }
}