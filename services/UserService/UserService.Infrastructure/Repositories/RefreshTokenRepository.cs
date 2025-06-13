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
    public class RefreshTokenRepository : Repository<RefreshToken, Guid, UserDbContext>, IRefreshTokenRepository
    {
        private readonly DbSet<RefreshToken> _refresh;
        public RefreshTokenRepository(UserDbContext dbContext) : base(dbContext)
        {
            _refresh = dbContext.Set<RefreshToken>();
        }
    }
}
