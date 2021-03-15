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
    public class OrderRepository: EFBaseRepository<AppDbContext, Domain.App.Order, DAL.App.DTO.Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext repoDbContext) : base(repoDbContext, new OrderMapper())
        {
        }
        
        public async Task<IEnumerable<DTO.Order>> GetAllPaginatedAsync(int pageNumber, int pageSize,
            bool noTracking = true)
        {
            var query = PrepareQuery(noTracking)
                .Include(o => o.OrderRows)
                .ThenInclude(or => or.Product)
                .ThenInclude(p => p!.Prices!.Where(pc => pc.To >= DateTime.UtcNow || pc.To == null))
                .OrderBy(p => p.Time);
            if (pageSize < 1)
            {
                return new List<DTO.Order>();
            }

            var page = pageNumber - 1 > 0 ? pageNumber - 1 : 0;
            
            var domainEntities = await query
                .Skip(page  * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return domainEntities.Select(e => Mapper.Map(e));
        }
    }
}