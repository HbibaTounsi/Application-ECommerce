using Application_ECommerce.App.Coupons.Dtos;
using Application_ECommerce.Core.Entities.Coupon;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_ECommerce.App.Coupons.Mapping
{
    public class CouponProfile : Profile
    {
        public CouponProfile()
        {
            // Entity <-> DTO
            CreateMap<Coupon, CouponDto>();
            CreateMap<CouponDto, Coupon>();

            // Create/Update
            CreateMap<CreateCouponDto, Coupon>();
            CreateMap<UpdateCouponDto, Coupon>();
        }
    }
}