using System.Threading.Tasks;
using NUnit.Framework;

namespace Application.IntegrationTests
{
    using static TestingFixture;

    public class TestBase
    {
        [SetUp]
        public async Task TestSetUp()
        {
            await ResetState();
        }

        [TearDown]
        public async Task TestTearDown()
        {
            await ResetState();
        }
    }
}