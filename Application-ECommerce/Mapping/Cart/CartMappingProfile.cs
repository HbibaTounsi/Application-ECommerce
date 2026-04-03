using Application_ECommerce.App.Cart.Dtos;
using Application_ECommerce.App.Orders.Dtos;
using Application_ECommerce.Models.Cart;
using Application_ECommerce.Models.Order;
using AutoMapper;

namespace Application_ECommerce.Mapping.Cart
{
     public class CartMappingProfile : Profile
    {
        public CartMappingProfile()
        {
            CreateMap<CartDto, CartIndexViewModel>().ReverseMap();
            CreateMap<CartDto, CheckoutCartViewModel>().ReverseMap();
            CreateMap<CartHeaderDto, OrderHeaderDto>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<CartDetailsDto, OrderDetailsDto>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product != null ? src.Product.Price : (src.Price ?? 0m)));
        }
    }
}