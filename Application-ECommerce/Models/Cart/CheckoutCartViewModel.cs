using Application_ECommerce.App.Cart.Dtos;

namespace Application_ECommerce.Models.Cart
{
    public class CheckoutCartViewModel
    {
        public CartHeaderDto CartHeader { get; set; } = new CartHeaderDto();
        public List<CartDetailsDto> CartDetails { get; set; } = new List<CartDetailsDto>();
    }
}
