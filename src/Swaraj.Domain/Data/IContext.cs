using System.Threading;
using System.Threading.Tasks;

using Swaraj.Domain.Entities;
using Swaraj.Domain.Entities.Identifiers;

namespace Swaraj.Domain.Data
{
    public interface IContext<T, V>
        where T : BaseEntity<V>
        where V : EntityIdentifier
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
