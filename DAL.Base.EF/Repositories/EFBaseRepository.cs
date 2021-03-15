using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.Base.Mappers;
using Contracts.DAL.Base.Repositories;
using Contracts.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace DAL.Base.EF.Repositories
{
    public class EFBaseRepository<TDbContext, TDomainEntity, TDALEntity>:
        EFBaseRepository<Guid, TDbContext, TDomainEntity, TDALEntity>,
        IBaseRepository<TDALEntity>
        where TDALEntity : class, IDomainEntityId<Guid>, new()
        where TDomainEntity : class, IDomainEntityId<Guid>, new()
        where TDbContext : DbContext
    {
        public EFBaseRepository(TDbContext repoDbContext, IBaseMapper<TDomainEntity, TDALEntity> mapper) : base(
            repoDbContext, mapper)
        {
        }
    }

    public class EFBaseRepository<TKey, TDbContext, TDomainEntity, TDALEntity> : IBaseRepository<TKey, TDALEntity>
        where TDALEntity : class, IDomainEntityId<TKey>, new()
        where TDomainEntity : class, IDomainEntityId<TKey>, new()
        where TDbContext : DbContext
        where TKey : IEquatable<TKey>
    {
        protected readonly TDbContext RepoDbContext;
        protected readonly DbSet<TDomainEntity> RepoDbSet;
        protected readonly IBaseMapper<TDomainEntity, TDALEntity> Mapper;

        public EFBaseRepository(TDbContext repoDbContext, IBaseMapper<TDomainEntity, TDALEntity> mapper)
        {
            RepoDbContext = repoDbContext;
            RepoDbSet = RepoDbContext.Set<TDomainEntity>();
            Mapper = mapper;
            
            if (RepoDbSet == null)
            {
                throw new ArgumentNullException(typeof(TDALEntity).Name + " was not found as DbSet!");
            }
        }

        public virtual async Task<IEnumerable<TDALEntity>> GetAllAsync(bool noTracking = true)
        {
            var domainEntities = await PrepareQuery(noTracking).ToListAsync();
            return domainEntities.Select(e => Mapper.Map(e));
        }

        public virtual async Task<TDALEntity?> FirstOrDefaultAsync(TKey id, bool noTracking = true)
        {
            var domainEntity = await PrepareQuery(noTracking).FirstOrDefaultAsync(e => e.Id.Equals(id));
            return domainEntity == null ? null : Mapper.Map(domainEntity);
        }

        public virtual TDALEntity Add(TDALEntity entity)
        {
            var trackedDomainEntity = RepoDbSet.Add(Mapper.Map(entity)).Entity;
            return Mapper.Map(trackedDomainEntity);
        }

        public virtual TDALEntity UpdateAsync(TDALEntity entity)
        {
            var trackedDomainEntity = RepoDbSet.Update(Mapper.Map(entity)).Entity;
            return Mapper.Map(trackedDomainEntity);
        }

        public virtual TDALEntity Remove(TDALEntity entity)
        {
            var domainEntity = Mapper.Map(entity);
            return Mapper.Map(RepoDbSet.Remove(domainEntity).Entity);
        }

        public virtual async Task<TDALEntity> RemoveAsync(TKey id)
        {
            var domainEntity = await PrepareQuery()
                .FirstOrDefaultAsync(e => e.Id.Equals(id));
            if (domainEntity == null)
            {
                throw new ArgumentException("Entity to be updated was not found in data source!");
            }

            return Mapper.Map(RepoDbSet.Remove(domainEntity).Entity);
        }

        public virtual async Task<bool> ExistsAsync(TKey id)
        {
            return await PrepareQuery().AnyAsync(e => e.Id.Equals(id));
        }

        protected IQueryable<TDomainEntity> PrepareQuery(bool noTracking = true)
        {
            var query = RepoDbSet.AsQueryable();

            if (noTracking)
            {
                query = query.AsNoTracking();
            }

            return query;
        }
    }
}