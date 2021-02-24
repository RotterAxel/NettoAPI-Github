using FluentAssertions;
using NUnit.Framework;

namespace Application.IntegrationTests.Common.Services
{
    using static TestingFixture;
    
    public class AESCryptographyServiceTests : TestBase
    {
        [Test]
        public void ShouldEncryptAndDecryptString()
        {
            string message = "Test message";

            string encryptedMessage = AESEncrypt(message);

            string decryptedMessage = AESDecrypt(encryptedMessage);

            decryptedMessage.Should().Be(message);
        }
    }
}