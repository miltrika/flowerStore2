using AutoMapper;
using DAL.App.DTO;
using DAL.Base.Mappers;

namespace DAL.App.EF.Mappers
{
    public class OrderMapper: BaseMapper<Domain.App.Order, Order>
    {
        public OrderMapper()
        {
            MapperConfigurationExpression.CreateMap<Domain.App.Product, Product>();
            MapperConfigurationExpression.CreateMap<Product, Domain.App.Product>();
            
            MapperConfigurationExpression.CreateMap<Domain.App.OrderRow, OrderRow>();
            MapperConfigurationExpression.CreateMap<OrderRow, Domain.App.OrderRow>();
            
            MapperConfigurationExpression.CreateMap<Domain.App.Price, Price>();
            MapperConfigurationExpression.CreateMap<Price, Domain.App.Price>();

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}