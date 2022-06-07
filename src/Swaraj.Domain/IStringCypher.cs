namespace Swaraj.Domain
{
    public interface IStringCypher
    {
        string Encrypt(string text);
        string Decrypt(string text);
    }
}
