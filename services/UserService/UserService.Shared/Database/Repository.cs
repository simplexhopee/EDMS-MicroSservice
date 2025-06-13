using Microsoft.EntityFrameworkCore;

namespace UserService.Shared.Database
{
    public class Repository<TEntity, TKey, TContext> : IRepository<TEntity, TKey> where TContext : DbContext where TEntity : class
    {
        private readonly TContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;
        public Repository(TContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
           return await _dbSet.ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(TKey id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task SaveAsync()
        {
           await _dbContext.SaveChangesAsync();
        }

        public void Update(TEntity entity)
        {
            _dbContext.Update(entity);
        }
    }
}
