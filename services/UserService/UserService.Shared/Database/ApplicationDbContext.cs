using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UserService.Shared.Common;
using UserService.Shared.MultiTenancy;
using UserService.Shared.Auth;

namespace UserService.Shared.Database
{
    public class ApplicationDbContext : DbContext
    {
        private readonly ITenantContext _tenantContext;
        private readonly ICurrentUser _currentUser;
        public ApplicationDbContext(
             DbContextOptions options,
            ITenantContext tenantContext, 
            ICurrentUser currentUser)
            : base(options)
        {
            _tenantContext = tenantContext;
            _currentUser = currentUser;

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach(var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(IMultiTenant).IsAssignableFrom(entityType.ClrType))
                {
                    var tenantId = _tenantContext.TenantId;
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var tenantProperty = Expression.Property(parameter, nameof(IMultiTenant.TenantId));
                    var tenantConstant = Expression.Constant(tenantId);
                    var equality = Expression.Equal(tenantProperty, tenantConstant);

                    var lambda = Expression.Lambda(equality, parameter);
                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
                }
            }
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is IAuditable &&
                       (e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted));

            foreach (var entry in entries)
            {
                var auditable = (IAuditable)entry.Entity;
                if (entry.State == EntityState.Added)
                {
                    auditable.CreatedAt = DateTime.UtcNow;
                    auditable.CreatedBy = _currentUser.Email;
                }
                else if (entry.State == EntityState.Modified) 
                {
                    auditable.UpdatedAt = DateTime.UtcNow;
                    auditable.UpdatedBy = _currentUser.Email;
                }
                else if(entry.State == EntityState.Deleted)
                {
                    auditable.DeletedAt = DateTime.UtcNow;
                    auditable.DeletedBy = _currentUser.Email;
                }
                
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

       
    }
}
