using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Domain.Entities;
using UserService.Domain.Interfaces;
using UserService.Shared.MultiTenancy;

namespace UserService.Infrastructure.DbContexts
{
    public class UserDbContext : ApplicationDbContext
    {
        public UserDbContext(ITenantContext tenantContext, ICurrentUser currentUser) : base(tenantContext, currentUser)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
