using System;
using System.Linq;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Infrastructure.Services
{
    public class RandomStringGenerator : IRandomStringGenerator
    {
        private Random _random;

        private const string UppercaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string LowercaseLetters = "abcdefghijklmnopqrstuvwxyz";
        private const string Digits = "0123456789";

        public RandomStringGenerator()
        {
            _random = new Random();
        }
        
        /// <summary>
        /// Generates a string of x length, with uppercase, lowercase or digits
        /// </summary>
        /// <param name="length"></param>
        /// <param name="includeUppercaseLetters"></param>
        /// <param name="includeLowercaseLetters"></param>
        /// <param name="includeDigits"></param>
        /// <exception cref="InvalidOperationException">At least one bool must be true</exception>
        /// <exception cref="ArgumentOutOfRangeException">Length is 0 or less than 0</exception>
        /// <returns>By default will return random digits of length 1</returns>
        public string GenerateRandomStringOfLength(int length, bool includeUppercaseLetters, bool includeLowercaseLetters, bool includeDigits)
        {
            string charactersToRandomize = "";

            if (length <= 0)
                throw new ArgumentOutOfRangeException($"Length paramater of {length} should be > 0");
            
            if (!includeDigits && !includeLowercaseLetters && !includeUppercaseLetters)
                throw new InvalidOperationException("At least one bool should be true");

            if (includeDigits)
                charactersToRandomize += Digits;
                
            if (includeLowercaseLetters)
                charactersToRandomize += LowercaseLetters;

            if (includeUppercaseLetters)
                charactersToRandomize += UppercaseLetters;
            
            return new string(Enumerable.Repeat(charactersToRandomize, length)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }
    }
}