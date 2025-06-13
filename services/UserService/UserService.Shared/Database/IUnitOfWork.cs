using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Shared.Database
{
    public interface IUnitOfWork: IDisposable
    {
        IRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : class;
        Task CompleteAsync();
    }
}
