using AutoMapper;
using DAL.App.DTO;
using DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class ProductMapper: BaseMapper<Domain.App.Product, Product>
    {
        public ProductMapper()
        {
            MapperConfigurationExpression.CreateMap<Domain.App.Price, Price>();
            MapperConfigurationExpression.CreateMap<Price, Domain.App.Price>();

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}