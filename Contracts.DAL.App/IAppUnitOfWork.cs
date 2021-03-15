using Contracts.DAL.App.Repositories;
using Contracts.DAL.Base;

namespace Contracts.DAL.App
{
    public interface IAppUnitOfWork: IBaseUnitOfWork
    {
        IProductRepository Products { get; }
        IOrderRepository Orders { get; }
        IOrderRowRepository OrderRows { get; }
        IPriceRepository Prices { get; }
    }
}