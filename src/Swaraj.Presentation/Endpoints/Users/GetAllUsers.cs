using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Ardalis.ApiEndpoints;
using Swaraj.Domain.Data;

using Swaraj.Domain;
using Swaraj.Domain.Entities;
using Swaraj.Presentation.Endpoints.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

namespace Swaraj.Presentation.Endpoints.Users
{
    [ApiController]
    [Route("api/users")]
    public class GetAllUsers :
         BaseAsyncEndpoint
        .WithoutRequest
        .WithResponse<IEnumerable<User>>
    {
        private readonly IUserContext _context;

        public GetAllUsers(IUserContext context)
        {
            _context = context;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get all users.", Tags = new[] { "Users" })]
        public override async Task<ActionResult<IEnumerable<User>>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var users = await _context.Users.ToListAsync();

            return users;
        }
    }
}
