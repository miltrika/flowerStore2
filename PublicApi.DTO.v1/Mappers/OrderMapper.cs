using System.Linq;
using AutoMapper;
using DAL.Base.Mappers;

namespace PublicApi.DTO.v1.Mappers
{
    public class OrderMapper : BaseMapper<DAL.App.DTO.Order, Order>
    {
        public OrderMapper()
        {
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.OrderRow, OrderRow>()
                .ForMember(dest => dest.ProductName, opt =>
                    opt.MapFrom(dto => dto.Product!.Name))
                .ForMember(dest => dest.ProductPrice, opt =>
                    opt.MapFrom(dto =>
                        dto.Product!.Prices!.First().Amount));

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}