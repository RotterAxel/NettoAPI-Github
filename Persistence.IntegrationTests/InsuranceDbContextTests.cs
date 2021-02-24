using System;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities.Insurance;
using Infrastructure.Persistence.DbContexts.Insurance;
using Microsoft.EntityFrameworkCore;
using Moq;
using Shouldly;
using Xunit;

namespace Persistence.IntegrationTests
{
    public class InsuranceDbContextTests
    {
         private readonly string _userId;
        private readonly DateTime _dateTime;
        private readonly Mock<IDateTime> _dateTimeMock;
        private readonly Mock<ICurrentUserService> _currentUserServiceMock;
        private readonly IInsuranceDbContext _sut;

        public InsuranceDbContextTests()
        {
            _dateTime = new DateTime(3001, 1, 1);
            _dateTimeMock = new Mock<IDateTime>();
            _dateTimeMock.Setup(m => m.UtcNow).Returns(_dateTime);

            _userId = "00000000-0000-0000-0000-000000000000";
            _currentUserServiceMock = new Mock<ICurrentUserService>();
            _currentUserServiceMock.Setup(m => m.KeycloakUserId).Returns(_userId);

            var options = new DbContextOptionsBuilder<InsuranceDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            _sut = new InsuranceDbContext(options, _dateTimeMock.Object, _currentUserServiceMock.Object);

            var land = new Land
            {
                Name = "Land1"
            };

            _sut.Länder.Add(land);

            _sut.SaveChanges();
        }

        [Fact]
        public void SaveChangesAsync_GivenNewLand_ShouldSetCreatedProperties()
        {
            var land = new Land
            {
                Name = "Land"
            };

            _sut.Länder.Add(land);

            _sut.SaveChanges();

            land.CreatedOn.ShouldBe(_dateTime);
            land.CreatedBy.ShouldBe(_userId); 
        }

        [Fact]
        public async Task SaveChangesAsync_GivenExistingLand_ShouldSetLastModifiedAndModifiedByProperties()
        {
            var land = await _sut.Länder.FirstAsync();
        
            land.Name = "New Land";
        
            _sut.SaveChanges();
        
            land.RowVersion.ShouldNotBeNull();
            land.RowVersion.ShouldBe(_dateTime);
            land.ModifiedBy.ShouldBe(_userId);
        }
    }
}