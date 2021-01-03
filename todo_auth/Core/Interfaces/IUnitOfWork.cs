using Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace todo_auth.Core.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();
        int SaveChanges();

        void BeginTransaction();

        void CommitTransaction();

        void RollbackTransaction();

        IRepository<T> GetRepository<T>()
            where T : BaseEntity;
    }
}
