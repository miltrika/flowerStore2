using System.Linq;
using AutoMapper;
using DAL.Base.Mappers;

namespace PublicApi.DTO.v1.Mappers
{
    public class ProductMapper: BaseMapper<DAL.App.DTO.Product, Product>
    {
        public ProductMapper()
        {
            MapperConfigurationExpression.CreateMap<DAL.App.DTO.Product, Product>()
                .ForMember(dest => dest.Price, opt =>
                    opt.MapFrom(dto => dto.Prices!.First().Amount));

            Mapper = new Mapper(new MapperConfiguration(MapperConfigurationExpression));
        }
    }
}