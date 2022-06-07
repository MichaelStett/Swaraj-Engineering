using System.Net;
using System.Threading;
using System.Threading.Tasks;

using Ardalis.ApiEndpoints;

using Microsoft.AspNetCore.Mvc;

using Swaraj.Domain;
using Swaraj.Domain.Data;
using Swaraj.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Swaraj.Presentation.Endpoints.Authorization
{
    public record RegisterDto(string Username, string Email, string Password);

    sealed record RegisterExample : RegisterDto, IExamplesProvider<RegisterDto>
    {
        public RegisterExample()
            : base("exampleName", "example@mail.com", "password")
        { }

        public RegisterDto GetExamples() => this;
    }

    [ApiController]
    [Route("api/register")]
    public class Register :
         BaseAsyncEndpoint
        .WithRequest<RegisterDto>
        .WithResponse<User>
    {
        private readonly IStringCypher _cypher;
        private readonly IUserContext _context;

        public Register(IStringCypher cypher, IUserContext context)
        {
            _context = context;
            _cypher = cypher;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Register user.", Tags = new[] { "Authorization" })]
        [SwaggerResponse((int) HttpStatusCode.OK, "User sucessfuly registered.")]
        [SwaggerRequestExample(typeof(RegisterExample), typeof(RegisterExample))]
        public override async Task<ActionResult<User>> HandleAsync(
            [FromBody] RegisterDto request, CancellationToken cancellationToken = default)
        {
            User user = new();

            user.Username = request.Username;
            user.Email = request.Email;
            user.Password = _cypher.Encrypt(request.Password);

            await _context.Users.AddAsync(user);

            return Ok(user);
        }
    }
}
