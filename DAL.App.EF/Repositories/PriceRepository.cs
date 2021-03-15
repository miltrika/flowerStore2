using Contracts.DAL.App.Repositories;
using DAL.Base.EF.Repositories;
using DAL.Base.Mappers;
using Domain.App;

namespace DAL.App.EF.Repositories
{
    public class PriceRepository: EFBaseRepository<AppDbContext, Domain.App.Price, DAL.App.DTO.Price>, IPriceRepository
    {
        public PriceRepository(AppDbContext repoDbContext) : base(repoDbContext, new BaseMapper<Price, DTO.Price>())
        {
        }
        
        
    }
}