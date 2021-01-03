using Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace todo_auth.Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> ListAllAsync();
        IQueryable<T> AsQueryable();
        T Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(object id);
        Task<T> GetAsync(object id);
    }
}
