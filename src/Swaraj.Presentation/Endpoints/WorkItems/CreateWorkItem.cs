using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

using Ardalis.ApiEndpoints;

using Microsoft.AspNetCore.Mvc;

using Swaraj.Domain.Data;
using Swaraj.Domain.Entities;

using Swashbuckle.AspNetCore.Annotations;

namespace Swaraj.Presentation.Endpoints.WorkItems
{

    [Route("api/workitems")]
    public class CreateWorkItem :
        BaseAsyncEndpoint
        .WithRequest<WorkItem>
        .WithoutResponse
    {
        private readonly IWorkItemContext _context;

        public CreateWorkItem(IWorkItemContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Consumes("application/json")]
        [SwaggerOperation(Summary = "Add new WorkItem.", Tags = new[] { "WorkItems" })]
        [SwaggerResponse((int)HttpStatusCode.OK, "Sucessfully added WorkItem")]
        public override async Task<ActionResult> HandleAsync(WorkItem request, CancellationToken cancellationToken = default)
        {
            try
            {
                var wi = WorkItem.Clone(request);

                await _context.WorkItems.AddAsync(wi, cancellationToken);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }
    }
}
