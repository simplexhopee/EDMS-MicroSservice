using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Shared.Database
{
    public class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        private readonly Dictionary<Type, object> _repositories = new();
        private readonly TContext _dbContext;
        private readonly IServiceProvider _serviceProvider;

        public UnitOfWork(TContext dbContext, IServiceProvider serviceProvider)
        {
            _dbContext = dbContext;
            _serviceProvider = serviceProvider;
        }

        public IRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : class
        {
            var type = typeof(TEntity);
            if(!_repositories.ContainsKey(type))
            {
                var repo = _serviceProvider.GetService(typeof(IRepository<TEntity, TKey>)) as IRepository<TEntity, TKey>;
                if (repo != null) _repositories.Add(type, repo);
            }
            return (IRepository<TEntity, TKey>)_repositories[type];
        }
        public async Task CompleteAsync()
        {
           await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

       
    }
}
