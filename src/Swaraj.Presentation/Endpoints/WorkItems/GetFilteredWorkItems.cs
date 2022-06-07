using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Ardalis.ApiEndpoints;

using Microsoft.AspNetCore.Mvc;

using Swaraj.Domain.Entities;
using Swaraj.Domain.Enums;

using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using Swaraj.Domain.Data;
using Microsoft.EntityFrameworkCore;

namespace Swaraj.Presentation.Endpoints.WorkItems
{
    public record WorkItemFilterProperties(
        string AssignedTo = null,
        string CreatedBy = null,
        string Title = null,
        WorkItemState? WorkItemState = null,
        WorkItemType? WorkItemType = null,
        int Page = 1,
        int Limit = 10
    );

    sealed record WorkItemFilterPropertiesExample() : WorkItemFilterProperties, IExamplesProvider<WorkItemFilterProperties>
    {
        public WorkItemFilterProperties GetExamples()
            => this;
    }

    //[Authorize]
    [Route("api/workitems/filter")]
    public class GetFilteredWorkItems :
        BaseAsyncEndpoint
        .WithRequest<WorkItemFilterProperties>
        .WithResponse<IEnumerable<WorkItem>>
    {
        private readonly IWorkItemContext _context;

        public GetFilteredWorkItems(IWorkItemContext context)
        {
            _context = context;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Get all workitems", Tags = new[] { "WorkItems" })]
        [SwaggerResponse((int) HttpStatusCode.OK, "Sucessfully fetched WorkItems")]
        [SwaggerRequestExample(typeof(WorkItemFilterPropertiesExample), typeof(WorkItemFilterPropertiesExample))]

        public override async Task<ActionResult<IEnumerable<WorkItem>>> HandleAsync([FromQuery] WorkItemFilterProperties request, CancellationToken cancellationToken = default)
        {
            IEnumerable<WorkItem> workItems = await _context.WorkItems.ToListAsync();

            IEnumerable<WorkItem> filteredWorkItems = workItems
                .Where(w =>
                    w.AssignedTo.Username == request.AssignedTo || request.AssignedTo == null &&
                    w.CreatedBy == request.CreatedBy || request.CreatedBy == null &&
                    w.Title.Contains(request.Title) || request.Title == null &&
                    w.WorkItemState == request.WorkItemState || request.WorkItemState == null &&
                    w.WorkItemType == request.WorkItemType || request.WorkItemType == null
                ).Skip(
                    (request.Page - 1)*request.Limit
                ).Take(
                    request.Limit
                );

            return Ok(filteredWorkItems);
        }
    }
}
