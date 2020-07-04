using RentCar.DAL.SQL;
using RentCar.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RentCar.Persistence.Generic
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {

        protected RentCarContext context { get; private set; }

        public BaseRepository(RentCarContext dbContext)
        {
            context = dbContext;
        }

        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await context.Set<TEntity>().Where(predicate).FirstOrDefaultAsync();
        }

        public virtual async Task<TEntity> GetAsync(int id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await context.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await context.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public async Task AddAsync(TEntity entity)
        {
            var entry = context.Set<TEntity>();

            entry.Add(entity);

            await context.SaveChangesAsync();
        }

        public async Task AddAllAsync(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                await AddAsync(entity);
            }
        }

        public void Remove(TEntity entity)
        {
            var entry = context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                context.Set<TEntity>().Attach(entity);
            }
            context.Entry<TEntity>(entity).State = EntityState.Deleted;
        }

        public void RemoveAll(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Remove(entity);
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            return context.Set<TEntity>().ToList();
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return context.Set<TEntity>().Where(predicate).ToList();
        }



        public int Total(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
            {
                return context.Set<TEntity>().Count();
            }
            else
            {
                return context.Set<TEntity>().Count(predicate);
            }
        }

        public List<int> GetPages(int amountOfPages, Expression<Func<TEntity, bool>> predicate = null)
        {
            var total = Math.Ceiling((double)Total(predicate) / amountOfPages);

            var pages = new List<int>();
            for (int i = 0; i < total; i++)
            {
                pages.Add(i + 1);
            }

            if (pages.Count == 0)
                pages.Add(1);

            return pages;
        }


    }
}
