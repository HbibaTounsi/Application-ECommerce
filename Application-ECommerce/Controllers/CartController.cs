using Application_ECommerce.App.Cart.Interfaces;
using Application_ECommerce.App.Coupons.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application_ECommerce.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        private readonly ICouponService _couponService;
         private readonly IOrderServices _orderServices;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public CartController(ICartService cartService, ICouponService couponService, IOrderServices orderServices, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _cartService = cartService;
            _couponService = couponService;
            _orderServices = orderServices;
            _userManager = userManager;
            _mapper = mapper;
        }

    }
}
