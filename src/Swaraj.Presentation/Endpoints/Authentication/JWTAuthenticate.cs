using System.Net;
using System.Threading;
using System.Threading.Tasks;

using Ardalis.ApiEndpoints;

using Microsoft.AspNetCore.Mvc;

using Swaraj.Domain.JWTAuthentication;
using Swaraj.Domain.Services;

using Swashbuckle.AspNetCore.Annotations;

using Swashbuckle.AspNetCore.Filters;

namespace Swaraj.Presentation.Endpoints.Authentication
{
    sealed class AuthenticateRequestExample : AuthenticateRequest, IExamplesProvider<AuthenticateRequest>
    {
        public AuthenticateRequestExample()
        {
            Username = "exampleName";
            Password = "password";
        }

        public AuthenticateRequest GetExamples() => this;
    }

    [ApiController]
    [Route("api/authenticate")]
    public class JWTAuthenticate :
        BaseAsyncEndpoint
        .WithRequest<AuthenticateRequest>
        .WithResponse<AuthenticateResponse>
    {
        private readonly IAuthenticationService _userService;

        public JWTAuthenticate(IAuthenticationService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Obtain authentication token.", Tags = new[] { "Authentication" })]
        [SwaggerResponse((int)HttpStatusCode.OK, "Sucessfuly obtained token.")]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Something weant wrong...")]
        [SwaggerRequestExample(typeof(AuthenticateRequestExample), typeof(AuthenticateRequestExample))]
        public override async Task<ActionResult<AuthenticateResponse>> HandleAsync(AuthenticateRequest request, CancellationToken cancellationToken = default)
        {
            var response = _userService.Authenticate(request);

            if (response == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            return Ok(response);
        }
    }
}
