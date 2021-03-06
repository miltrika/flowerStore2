using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IOrderRepository: IBaseRepository<Order>
    {
        Task<IEnumerable<Order>> GetAllPaginatedAsync(int pageNumber, int pageSize,
            bool noTracking = true);
    }
}