using AutoMapper;
using Vubiquity.Test.Domain.Models;

namespace Vubiquity.Test.BasketAPI.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Data.Models.Basket, BasketModel>().ForMember(
                dest => dest.Items,
                opt => opt.MapFrom(src => src.Items)
                );
            CreateMap<BasketModel, Data.Models.Basket>().ForMember(
                dest => dest.Items,
                opt => opt.MapFrom(src => src.Items)
                );
            CreateMap<Data.Models.BasketItem, BasketItemModel>();
            CreateMap<BasketItemModel, Data.Models.BasketItem>();
            CreateMap<Data.Models.Product, ProductModel>();
        }
    }
}
