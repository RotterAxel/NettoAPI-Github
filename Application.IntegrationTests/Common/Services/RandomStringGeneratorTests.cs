using System;
using FluentAssertions;
using NUnit.Framework;

namespace Application.IntegrationTests.Common.Services
{
    using static TestingFixture;
    
    public class RandomStringGeneratorTests : TestBase
    {
        [Test]
        public void LengthSmallerEquals0_ShouldThrowArgumentOutOfRangeException()
        {
            FluentActions.Invoking(() =>
                GenerateRandomStringOfLength(-1, true, true, true))
                    .Should().Throw<ArgumentOutOfRangeException>();
            
            FluentActions.Invoking(() =>
                    GenerateRandomStringOfLength(0, true, true, true))
                .Should().Throw<ArgumentOutOfRangeException>();
        }


        [Test]
        public void AllFalseParameters_ShouldThrowInvalidOperationException()
        {
            FluentActions.Invoking(() =>
                    GenerateRandomStringOfLength(1, false, false, false))
                .Should().Throw<InvalidOperationException>();
        }
        
        [Test]
        public void AllTrueParameters_ShouldReturnStringWithLowercaseUppercaseAndDigits()
        {
            var randomString = 
                GenerateRandomStringOfLength(50, true, true, true);

            randomString.Should().ContainAny("A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P",
                "Q","R","S","T","U","V","W","X","Y","Z");
            randomString.Should().ContainAny("a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p",
                "q","r","s","t","u","v","w","x","y","z");
            randomString.Should().ContainAny("0","1","2","3","4","5","6","7","8","9");
        }
        
        [Test]
        public void AllTrueExceptLowecaseParameter_ShouldReturnStringWithUppercaseAndOrDigits()
        {
            var randomString = 
                GenerateRandomStringOfLength(50, true, false, true);

            randomString.Should().ContainAny("A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P",
                "Q","R","S","T","U","V","W","X","Y","Z","0","1","2","3","4","5","6","7","8","9");
            randomString.Should().NotContainAny("a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p",
                "q","r","s","t","u","v","w","x","y","z");
        }
        
        [Test]
        public void AllTrueExceptUpercaseParameter_ShouldReturnStringWithLowercaseAndOrDigits()
        {
            var randomString = 
                GenerateRandomStringOfLength(50, false, true, true);

            randomString.Should().ContainAny("0","1","2","3","4","5","6","7","8","9","a","b","c","d","e","f",
                "g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z");
            randomString.Should().NotContainAny("A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P",
                "Q","R","S","T","U","V","W","X","Y","Z");
        }
        
        [Test]
        public void AllTrueExceptDigitsParameter_ShouldReturnStringWithLowercaseAndOrLowercase()
        {
            var randomString = 
                GenerateRandomStringOfLength(50, true, true, false);

            randomString.Should().ContainAny("a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p",
                "q","r","s","t","u","v","w","x","y","z","A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P",
                "Q","R","S","T","U","V","W","X","Y","Z");
            randomString.Should().NotContainAny("0","1","2","3","4","5","6","7","8","9");
        }
    }
}