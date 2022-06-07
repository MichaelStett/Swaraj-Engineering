using System.Linq;

using Swaraj.Domain;
using Swaraj.Domain.Data;
using Swaraj.Domain.Entities;
using Swaraj.Domain.JWTAuthentication;
using Swaraj.Domain.Services;

namespace Swaraj.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserContext _context;
        private readonly IStringCypher _cypher;
        private readonly TokenGenerator _tokenGenerator;

        public AuthenticationService(
            IUserContext context,
            IStringCypher cypher,
            TokenGenerator tokenGenerator)
        {
            _context = context;
            _cypher = cypher;
            _tokenGenerator = tokenGenerator;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest request)
        {
            User user = _context.Users.SingleOrDefault(u => u.Username == request.Username && _cypher.Decrypt(u.Password) == request.Password);

            AuthenticateResponse response = null;

            if (user != null)
            {
                string token = _tokenGenerator.GenerateFor(user);

                response = new AuthenticateResponse(user, token);
            }

            return response;

        }
    }
}
