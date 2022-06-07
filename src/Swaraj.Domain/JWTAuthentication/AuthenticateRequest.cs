using System.ComponentModel.DataAnnotations;

namespace Swaraj.Domain.JWTAuthentication
{
    public class AuthenticateRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
