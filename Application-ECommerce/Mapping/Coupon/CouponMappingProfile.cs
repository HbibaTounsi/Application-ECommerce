using Application_ECommerce.App.Coupons.Dtos;
using Application_ECommerce.Models.Coupon;
using AutoMapper;

namespace Application_ECommerce.Mapping.Coupon
{
     public class CouponMappingProfile : Profile
    {
        public CouponMappingProfile()
        {
            CreateMap<CouponDto, CouponViewModel>().ReverseMap();
            CreateMap<CreateCouponViewModel, CouponDto>().ReverseMap();
            CreateMap<DeleteCouponViewModel, CouponDto>().ReverseMap();
        }
    }
}