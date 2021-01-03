using Entities.Base;
using Infrastructures;
using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todo_auth.Core.Interfaces;

namespace todo_auth.Repositories
{
    public class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly TodoDataContext _dbContext;

        public EfRepository(TodoDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public T Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            return entity;
        }

        public IQueryable<T> AsQueryable()
        {
            return _dbContext.Set<T>().AsQueryable();
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public void Delete(object id)
        {
            var item = _dbContext.Set<T>().Find(id);
            if (item != null)
            {
                Delete(item);
            }
        }

        public async Task<T> GetAsync(object id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
