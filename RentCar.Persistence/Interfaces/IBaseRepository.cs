using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Persistence.Interfaces
{

    public interface IBaseRepository<TEntity>
    {
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetAsync(int id);

        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate);

        Task AddAsync(TEntity entity);
        Task AddAllAsync(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveAll(IEnumerable<TEntity> entities);
    }
}
