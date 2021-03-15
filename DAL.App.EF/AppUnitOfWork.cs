using System;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Repositories;
using DAL.Base.EF;

namespace DAL.App.EF
{
    public class AppUnitOfWork : EFBaseUnitOfWork<Guid, AppDbContext>, IAppUnitOfWork
    {
        public AppUnitOfWork(AppDbContext uowDbContext) : base(uowDbContext)
        {
        }

        public IProductRepository Products =>
            GetRepository<IProductRepository>(() => new ProductRepository(UOWDbContext));
        public IOrderRepository Orders =>
            GetRepository<IOrderRepository>(() => new OrderRepository(UOWDbContext));

        public IOrderRowRepository OrderRows =>
            GetRepository<IOrderRowRepository>(() => new OrderRowRepository(UOWDbContext));

        public IPriceRepository Prices =>
            GetRepository<IPriceRepository>(() => new PriceRepository(UOWDbContext));
    }
}