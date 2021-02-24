namespace Application.Common.Interfaces
{
    public interface IAESCryptographyService
    {
        string EncryptString(string plainInput);
        string DecryptString(string cipherText);
    }
}