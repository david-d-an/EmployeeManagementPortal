namespace EMP.Core.Security
{
    public interface IAesCryptoUtil
    {
        string Decrypt(string base64String);
        string Encrypt(string text);
    }
}