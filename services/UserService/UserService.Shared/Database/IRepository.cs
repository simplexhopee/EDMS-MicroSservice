
namespace UserService.Shared.Database
{
    public interface IRepository<TEntity, TKey> where  TEntity : class
    {
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(TKey id);
        Task SaveAsync();

    }
}
