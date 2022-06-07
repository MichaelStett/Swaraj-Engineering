using Microsoft.EntityFrameworkCore;
using Swaraj.Domain.Entities;
using Swaraj.Domain.Entities.Identifiers;

namespace Swaraj.Domain.Data
{
    public interface IUserContext : IContext<User, UserIdentifier>
    {
        DbSet<User> Users { get; set; }
    }
}
