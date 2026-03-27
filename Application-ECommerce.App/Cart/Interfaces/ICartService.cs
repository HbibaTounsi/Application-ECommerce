using Application_ECommerce.App.Cart.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_ECommerce.App.Cart.Interfaces
{

    public interface ICartService
    {
        Task<CartDto?> GetCartByUserIdAsync(string userId);
        Task<CartDto> UpsertCartAsync(CartDto cartDto);
        Task<CartDto?> ApplyCouponAsync(string userId, string couponCode);
        Task<bool> RemoveCartItemAsync(Guid cartDetailsId);
        Task<bool> ClearCartAsync(string userId);
    }
}