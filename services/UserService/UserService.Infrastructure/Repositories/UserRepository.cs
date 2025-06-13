using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.Interfaces;
using UserService.Domain.Entities;
using UserService.Infrastructure.DbContexts;
using UserService.Shared.Database;

namespace UserService.Infrastructure.Repositories
{
    public class UserRepository : Repository<User,Guid, UserDbContext>, IUserRepository 
    {
        private readonly DbSet<User> _users;
        public UserRepository(UserDbContext dbContext) : base(dbContext)
        {
            _users = dbContext.Set<User>();
        }

        public async Task<bool> ExistsByEmailAndTenant(string email)
        {
           return await _users.AnyAsync(u => u.Email == email);
        }

        public async Task<User?> GetByEmail(string email)
        {
            return await _users.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
