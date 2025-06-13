using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;
using UserService.Shared.MultiTenancy;
using UserService.Shared.Auth;
using UserService.Shared.Database;

namespace UserService.Infrastructure.DbContexts
{
    public class UserDbContext : ApplicationDbContext
    {
        public UserDbContext(
            DbContextOptions<UserDbContext> options,
            ITenantContext tenantContext, 
            ICurrentUser currentUser) : base(options, tenantContext, currentUser)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}
