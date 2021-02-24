namespace Application.Common.Interfaces
{
    public interface IRandomStringGenerator
    {
        string GenerateRandomStringOfLength(int length, bool includeUppercaseLetters, bool includeLowercaseLetters,
            bool includeDigits);
    }
}