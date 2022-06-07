using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using Swaraj.Domain;
using Swaraj.Domain.Entities;

namespace Swaraj.Application
{
    public class TokenGenerator
    {
        private readonly AppSettings _appSettings;
        private readonly IDateTime _dateTime;

        public TokenGenerator(
            IOptions<AppSettings> options,
            IDateTime dateTime)
        {
            _appSettings = options.Value;
            _dateTime = dateTime;
        }

        public string GenerateFor(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            byte[] key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new[] { new Claim("id", user.Id.Value.ToString()) }),
                Expires = _dateTime.Now.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), 
                    SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
