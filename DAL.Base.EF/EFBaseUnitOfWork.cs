using System;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using Microsoft.EntityFrameworkCore;

namespace DAL.Base.EF
{
    public class EFBaseUnitOfWork<TKey, TDbContext> : BaseUnitOfWork<TKey> 
        where TDbContext: DbContext
        where TKey : IEquatable<TKey>
    {
        protected readonly TDbContext UOWDbContext;

        public EFBaseUnitOfWork(TDbContext uowDbContext)
        {
            this.UOWDbContext = uowDbContext;
        }

        public override async Task<int> SaveChangesAsync()
        {
            return await UOWDbContext.SaveChangesAsync();;
        }
    }
}