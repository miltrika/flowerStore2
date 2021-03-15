using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.Base.Repositories;
using DAL.App.DTO;

namespace Contracts.DAL.App.Repositories
{
    public interface IProductRepository: IBaseRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllPaginatedAsync(int pageNumber, int pageSize, bool noTracking = true);
        Task<IEnumerable<Product>> GetAllRelatedProductsByIdAndPopularity(Guid productId, bool noTracking = true);
    }
}