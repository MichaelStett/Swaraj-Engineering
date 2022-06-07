using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

using Ardalis.ApiEndpoints;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Swaraj.Domain.Data;
using Swaraj.Domain.Entities;

using Swashbuckle.AspNetCore.Annotations;

namespace Swaraj.Presentation.Endpoints.WorkItems
{
    [Route("api/workitems")]
    public class GetAllWorkItems :
        BaseAsyncEndpoint
        .WithoutRequest
        .WithResponse<IEnumerable<WorkItem>>
    {
        private readonly IWorkItemContext _context;

        public GetAllWorkItems(IWorkItemContext context)
        {
            _context = context;
        }

        //[Authorize]
        [HttpGet]
        [SwaggerOperation(Summary = "Get all workitems", Tags = new[] { "WorkItems" })]
        [SwaggerResponse((int) HttpStatusCode.OK, "Sucessfully fetched WorkItems")]
        public override async Task<ActionResult<IEnumerable<WorkItem>>> HandleAsync(CancellationToken cancellationToken = default)
        {
            IEnumerable<WorkItem> workItems = await _context.WorkItems.ToListAsync();

            return Ok(workItems);
        }
    }
}
