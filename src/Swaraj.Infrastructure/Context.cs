using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Swaraj.Domain;
using Swaraj.Domain.Data;
using Swaraj.Domain.Entities;
using Swaraj.Domain.Entities.Identifiers.Converters;

namespace Swaraj.Infrastructure
{
    public class Context : DbContext, IUserContext, IWorkItemContext
    {
        private readonly IDateTime _dateTime;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DbSet<User> Users { get; set; }
        public DbSet<WorkItem> WorkItems { get;  set; }

        public Context(
            DbContextOptions<Context> options,
            IHttpContextAccessor httpContextAccessor,
            IDateTime dateTime)
            : base(options)
        {
            _dateTime = dateTime;
            _httpContextAccessor = httpContextAccessor; 
        }


        protected override async void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder
                .Entity<WorkItem>()
                .Property(o => o.Id)
                .HasConversion(new WorkItemIdentifierValueConverter());

            builder
                .Entity<User>()
                .Property(o => o.Id)
                .HasConversion(new UserIdentifierValueConverter());

            
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddAuditInfo();

            return await base.SaveChangesAsync(cancellationToken);
        }

        private void AddAuditInfo()
        {
            IEnumerable<EntityEntry<IEntity>> entities = ChangeTracker.Entries<IEntity>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            DateTime utcNow = DateTime.UtcNow;
            string currentUser = _httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? string.Empty;
            string ipAddress = _httpContextAccessor?.HttpContext?.Connection?.RemoteIpAddress?.ToString();

            foreach (EntityEntry<IEntity> entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    entity.Entity.CreatedOnUtc = utcNow;
                    entity.Entity.CreatedBy = currentUser;
                }

                if (entity.State == EntityState.Modified)
                {
                    entity.Entity.LastModifiedOnUtc = utcNow;
                    entity.Entity.LastModifiedBy = currentUser;
                }

                entity.Entity.IPAddress = ipAddress;
            }
        }
    }
}
