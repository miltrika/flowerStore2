using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.Domain.Base;

namespace Contracts.DAL.Base.Repositories
{
    public interface IBaseRepository<TEntity>: IBaseRepository<Guid, TEntity>
        where TEntity: class, IDomainEntityId<Guid>, new()
    {
        
    }

    public interface IBaseRepository<in TKey, TEntity>
        where TKey : IEquatable<TKey>
        where TEntity : class, IDomainEntityId<TKey>, new()
    {
        Task<IEnumerable<TEntity>> GetAllAsync(bool noTracking = true);
        Task<TEntity?> FirstOrDefaultAsync(TKey id, bool noTracking = true);
        TEntity Add(TEntity entity);
        TEntity UpdateAsync(TEntity entity);
        TEntity Remove(TEntity entity);
        Task<TEntity> RemoveAsync(TKey id);
        Task<bool> ExistsAsync(TKey id);
    }
}