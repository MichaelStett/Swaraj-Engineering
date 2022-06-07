using Swaraj.Domain.JWTAuthentication;

namespace Swaraj.Domain.Services
{
    public interface IAuthenticationService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
    }
}
