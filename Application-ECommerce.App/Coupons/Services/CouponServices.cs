using Application_ECommerce.App.Coupons.Dtos;
using Application_ECommerce.App.Coupons.Interfaces;
using Application_ECommerce.Core.Entities.Coupon;
using Application_ECommerce.Core.Interfaces.Repositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_ECommerce.App.Coupons.Services
{
    public class CouponServices : ICouponService
    {
        private readonly ICouponRepository _couponRepository;
        private readonly IMapper _mapper;

        public CouponServices(ICouponRepository couponRepository, IMapper mapper)
        {
            _couponRepository = couponRepository;
            _mapper = mapper;
        }

       public async Task<CouponDto> AddAsync(CouponDto couponDto)
       {
        var coupon = _mapper.Map<Coupon>(couponDto);
        var added = await _couponRepository.AddAsync(coupon);
        return _mapper.Map<CouponDto>(added);  
       }

       public async Task<CouponDto?> ReadByIdAsync(Guid couponId)
       {
        var coupon = await _couponRepository.ReadByIdAsync(couponId);
        return coupon is null ? null : _mapper.Map<CouponDto>(coupon);
       }

       public async Task<CouponDto?> GetCouponByCodeAsync(string couponCode)
       {
            var coupon = await _couponRepository.ReadByCouponCodeAsync(couponCode);
            return coupon is null ? null : _mapper.Map<CouponDto>(coupon);
       }

         public async Task<IEnumerable<CouponDto>> ReadAllAsync()
         {
            var list = await _couponRepository.ReadAllAsync();
            return _mapper.Map<IEnumerable<CouponDto>>(list);
         }

          public async Task UpdateAsync(UpdateCouponDto updateCouponDto)
          {
            var existing = await _couponRepository.ReadByIdAsync(updateCouponDto.Id);
            if(existing == null)
            {
                throw new KeyNotFoundException("Coupon Not Found");
            }
            var toUpdate = _mapper.Map<Coupon>(updateCouponDto);
            await _couponRepository.UpdateAsync(toUpdate);  

          }

          public async Task DeleteAsync(Guid id)
          {
            var existing = await _couponRepository.ReadByIdAsync(id);
            if(existing == null)
            {
                throw new KeyNotFoundException("Coupon Not Found");
            }
            await _couponRepository.DeleteAsync(id);  
          }


       
    }
}
