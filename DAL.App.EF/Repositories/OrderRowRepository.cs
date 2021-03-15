using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Domain.App;

namespace DAL.App.EF.Repositories
{
    public class OrderRowRepository: EFBaseRepository<AppDbContext, Domain.App.OrderRow, DAL.App.DTO.OrderRow>, IOrderRowRepository
    {
        public OrderRowRepository(AppDbContext repoDbContext) : base(repoDbContext, new BaseMapper<OrderRow, DTO.OrderRow>())
        {
        }
    }
}