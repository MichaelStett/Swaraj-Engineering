using Microsoft.AspNetCore.DataProtection;

using Swaraj.Domain;

namespace Swaraj.Application
{
    public class StringCypher : IStringCypher
    {
        private const string Purpose = "my protection purpose";

        private readonly IDataProtectionProvider _provider;

        public StringCypher(IDataProtectionProvider provider)
        {
            _provider = provider;
        }

        public string Encrypt(string text)
        {
            IDataProtector protector = _provider.CreateProtector(Purpose);

            return protector.Protect(text);
        }

        public string Decrypt(string text)
        {
            IDataProtector protector = _provider.CreateProtector(Purpose);

            return protector.Unprotect(text);
        }
    }
}
