using Application_ECommerce.App.Cart.Dtos;
using Application_ECommerce.App.Cart.Interfaces;
using Application_ECommerce.App.Products.Dtos;
using Application_ECommerce.Core.Interfaces.Repositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_ECommerce.App.Cart.Services
{
     public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICouponRepository _couponRepository;
        private readonly IMapper _mapper;

         public CartService(ICartRepository cartRepository,
                           IMapper mapper,
                           IProductRepository productRepository,
                           ICouponRepository couponRepository)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
            _productRepository = productRepository;
            _couponRepository = couponRepository;
        }

public async Task<CartDto?> GetCartByUserIdAsync(string userId)
        {
            var cartHeaderFromDb = await _cartRepository.GetCartHeaderByUserIdAsync(userId);
            if (cartHeaderFromDb == null)
            {
                return null;
            }

            CartDto cartDto = new()
            {
                CartHeader = _mapper.Map<CartHeaderDto>(cartHeaderFromDb)
            };

            var cartDetailsFromDb = cartHeaderFromDb.CartDetails?.ToList() ?? new List<Core.Entities.Cart.CartDetails>();
            cartDto.CartDetails = _mapper.Map<IEnumerable<CartDetailsDto>>(cartDetailsFromDb);

            cartDto.CartHeader.CartTotal = 0;
            foreach (var item in cartDto.CartDetails)
            {
                var product = await _productRepository.ReadByIdAsync(item.ProductId);
                if (product != null)
                {
                    item.Product = _mapper.Map<ProductDto>(product);
                    item.Price = (item.Count * product.Price);
                    cartDto.CartHeader.CartTotal += item.Price;
                }
            }

            if (!string.IsNullOrEmpty(cartDto.CartHeader.CouponCode))
            {
                var coupon = await _couponRepository.ReadByCouponCodeAsync(cartDto.CartHeader.CouponCode);
                if (coupon != null && cartDto.CartHeader.CartTotal >= coupon.MinimumAmount)
                {
                    cartDto.CartHeader.Discount = coupon.DiscountAmount;
                    cartDto.CartHeader.CartTotal -= coupon.DiscountAmount;
                }
            }

            return cartDto;
        }

        public async Task<CartDto> UpsertCartAsync(CartDto cartDto)
        {
            var cartHeader = await _cartRepository.GetCartHeaderByUserIdAsync(cartDto.CartHeader.UserID);

            if (cartHeader == null)
            {
                cartHeader = _mapper.Map<Core.Entities.Cart.CartHeader>(cartDto.CartHeader);
                cartHeader = await _cartRepository.AddCartHeaderAsync(cartHeader);

                var first = cartDto.CartDetails.FirstOrDefault();
                if (first != null)
                {
                    first.CartHeaderId = cartHeader.Id;
                    var cartDetail = _mapper.Map<Core.Entities.Cart.CartDetails>(first);
                    await _cartRepository.AddCartDetailsAsync(cartDetail);
                }
            }
            else
            {
                var first = cartDto.CartDetails.FirstOrDefault();
                if (first != null)
                {
                    var cartDetailsFromDb = await _cartRepository.GetCartDetailsByCartHeaderIdAndProductId(cartHeader.Id, first.ProductId);
                    if (cartDetailsFromDb != null)
                    {
                        cartDetailsFromDb.Count += first.Count;
                        await _cartRepository.UpdateCartDetailsAsync(cartDetailsFromDb);
                    }
                    else
                    {
                        first.CartHeaderId = cartHeader.Id;
                        var cartDetail = _mapper.Map<Core.Entities.Cart.CartDetails>(first);
                        await _cartRepository.AddCartDetailsAsync(cartDetail);
                    }
                }
            }

            return (await GetCartByUserIdAsync(cartDto.CartHeader.UserID))!;
        }

        public async Task<CartDto?> ApplyCouponAsync(string userId, string couponCode)
        {
            var cartHeader = await _cartRepository.GetCartHeaderByUserIdAsync(userId);
            if (cartHeader != null)
            {
                if (couponCode == "")
                {
                    cartHeader.CouponCode = null;
                    cartHeader.CouponId = null;
                    await _cartRepository.UpdateCartHeaderAsync(cartHeader);
                    return await GetCartByUserIdAsync(userId);
                }

                var coupon = await _couponRepository.ReadByCouponCodeAsync(couponCode);
                if (coupon != null)
                {
                    cartHeader.CouponId = coupon.Id;
                    cartHeader.CouponCode = couponCode;
                    await _cartRepository.UpdateCartHeaderAsync(cartHeader);
                    return await GetCartByUserIdAsync(userId);
                }
                else
                {
                    throw new InvalidOperationException($"Coupon with code {couponCode} was not found.");
                }
            }
            return new CartDto();
        }

        public async Task<bool> RemoveCartItemAsync(Guid cartDetailsId)
        {
            var cartDetail = await _cartRepository.GetCartDetailsByCartDetailsId(cartDetailsId);
            if (cartDetail == null) return false;
            await _cartRepository.RemoveCartDetailsAsync(cartDetail);
            return true;
        }

        public async Task<bool> ClearCartAsync(string userId)
        {
            return await _cartRepository.ClearCartAsync(userId);
        }
    }
}
