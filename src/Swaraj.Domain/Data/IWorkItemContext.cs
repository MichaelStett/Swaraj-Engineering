using Microsoft.EntityFrameworkCore;
using Swaraj.Domain.Entities;
using Swaraj.Domain.Entities.Identifiers;

namespace Swaraj.Domain.Data
{
    public interface IWorkItemContext : IContext<WorkItem, WorkItemIdentifier>
    {
        DbSet<WorkItem> WorkItems { get; set; }
    }
}
