using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App.Repositories;
using DAL.App.EF.Mappers;
using DAL.Base.EF.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.Repositories
{
    public class ProductRepository : EFBaseRepository<AppDbContext, Domain.App.Product, DAL.App.DTO.Product>,
        IProductRepository
    {
        public ProductRepository(AppDbContext repoDbContext) : base(
            repoDbContext, new ProductMapper())
        {
        }

        public async Task<IEnumerable<DTO.Product>> GetAllPaginatedAsync(int pageNumber, int pageSize,
            bool noTracking = true)
        {
            var query = PrepareQuery(noTracking)
                .Where(p => p.Prices!.Count > 0)
                .Include(p => p.Prices!.Where(pc => pc.To >= DateTime.UtcNow || pc.To == null))
                .OrderBy(p => p.Name);
            if (pageSize < 1)
            {
                return new List<DTO.Product>();
            }

            var page = pageNumber - 1 > 0 ? pageNumber - 1 : 0;
            
            var domainEntities = await query
                .Skip(page  * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return domainEntities.Select(e => Mapper.Map(e));
        }

        public async Task<IEnumerable<DTO.Product>> GetAllRelatedProductsByIdAndPopularity(Guid productId, bool noTracking = true)
        {
            var query = PrepareQuery(noTracking);
            var domainEntities = await query
                .Where(p => p.Prices!.Count > 0 && p.OrderRows!
                                .Any(or => or.Order!.OrderRows!
                                    .Any(or2 => or2.ProductId == productId)) 
                            && p.Id != productId)
                .OrderByDescending(p => p.OrderRows!.Sum(or =>or.Quantity))
                .Include(p => p.Prices!.Where(pc => pc.To >= DateTime.UtcNow || pc.To == null))
                .ToListAsync();
            return domainEntities.Select(e => Mapper.Map(e));
        }
    }
}