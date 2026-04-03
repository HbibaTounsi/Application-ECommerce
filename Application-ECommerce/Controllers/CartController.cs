using Application_ECommerce.App.Cart.Dtos;
using Application_ECommerce.App.Cart.Interfaces;
using Application_ECommerce.App.Coupons.Interfaces;
using Application_ECommerce.App.Orders.Dtos;
using Application_ECommerce.App.Orders.Interfaces;
using Application_ECommerce.Core.Entities.Identity;
using Application_ECommerce.Core.Utility;
using Application_ECommerce.Models.Cart;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        public async Task<IActionResult> CartIndex()
        {
            var CartDto = await LoadCartDtoBasedOnLoggedInUser();
            if (CartDto == null)
            {
                return View(new CartIndexViewModel());
            }

            CartIndexViewModel vm = _mapper.Map<CartIndexViewModel>(CartDto);
            return View(vm);
        }
        public async Task<CartDto?> LoadCartDtoBasedOnLoggedInUser()
        {
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return new CartDto { CartHeader = new CartHeaderDto(), CartDetails = new List<CartDetailsDto>() };
            }
            return await _cartService.GetCartByUserIdAsync(userId);
        }

        [HttpPost]

        public async Task<IActionResult> ApplyCoupon(CartIndexViewModel model)
        {
            var couponCode = model.CartHeader.CouponCode;
            if (string.IsNullOrEmpty(couponCode))
            {
                TempData["Error"] = "Coupon invalide";
                return RedirectToAction(nameof(CartIndex));
            }
            try
            {
                var existingCoupon = await _couponService.GetCouponByCodeAsync(couponCode);
                if (existingCoupon == null)
                {
                    TempData["Error"] = "Coupon invalide";
                    return RedirectToAction(nameof(CartIndex));
                }

                var userId = _userManager.GetUserId(User);
                var CartDto = await _cartService.ApplyCouponAsync(userId, couponCode);
                if (CartDto == null)
                {
                    TempData["Error"] = "Coupon invalide";
                    return RedirectToAction(nameof(CartIndex));
                }
                TempData["Success"] = "Coupon appliqué avec succès";
                return RedirectToAction(nameof(CartIndex));
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(CartIndex));
            }
            catch
            {
                TempData["Error"] = "Une erreur est survenue";
                return RedirectToAction(nameof(CartIndex));
            }
        }

        public async Task<IActionResult> RemoveCoupon()
        {
            var userId = _userManager.GetUserId(User);
            var CartDto = await _cartService.ApplyCouponAsync(userId!, "");
            if (CartDto != null)
            {
                TempData["Success"] = "Coupon supprimé avec succès";
                return RedirectToAction(nameof(CartIndex));
            }
            TempData["Error"] = "Erreur lors de la suppression du coupon";
            return RedirectToAction(nameof(CartIndex));
        }
        public async Task<IActionResult> Remove(Guid cartDetailsId)
        {
            var removed = await _cartService.RemoveCartItemAsync(cartDetailsId);
            if (removed)
            {
                TempData["success"] = "Article supprimé du panier";
            }
            else
            {
                TempData["error"] = "Impossible de supprimer l'article";
            }
            return RedirectToAction(nameof(CartIndex));
        }


        public async Task<IActionResult> Checkout()
        {
            var cartDto = await LoadCartDtoBasedOnLoggedInUser();
            if (cartDto == null)
            {
                return View(new CheckoutCartViewModel());
            }
            var vm = _mapper.Map<CheckoutCartViewModel>(cartDto);
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutCartViewModel model)
        {
            if (model.CartDetails == null || !model.CartDetails.Any())
            {
                TempData["error"] = "Les détails du panier n'ont pas été transmis. Veuillez réessayer.";
                return RedirectToAction(nameof(Checkout));
            }
            var userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                TempData["error"] = "Vous devez être connecté pour passer commande.";
                return RedirectToAction("Login", "Auth");
            }
            var orderHeaderDto = _mapper.Map<OrderHeaderDto>(model.CartHeader);
            orderHeaderDto.UserId = userId;
            orderHeaderDto.Status = StaticDetails.Status_Pending;
            orderHeaderDto.OrderTime = DateTime.Now;
            orderHeaderDto.OrderTotal = model.CartHeader.CartTotal;
            var orderDetailsDto = _mapper.Map<List<OrderDetailsDto>>(model.CartDetails);
            // Normalize unit prices and counts from the posted model to ensure valid values for persistence
            foreach (var d in orderDetailsDto)
            {
                var posted = model.CartDetails.FirstOrDefault(x => x.ProductId == d.ProductId);
                if (posted != null)
                {
                    var total = posted.Price ?? 0m;
                    var cnt = posted.Count;
                    d.Count = cnt;
                    d.Price = cnt > 0 ? (total / cnt) : total;
                }
            }
            orderHeaderDto.OrderDetails = orderDetailsDto;

            try
            {
                var orderHeaderFromDb = await _orderServices.AddOrderHeaderAsync(orderHeaderDto);
                if (orderHeaderFromDb != null)
                {
                    foreach (var detail in orderDetailsDto)
                    {
                        detail.OrderHeaderId = orderHeaderFromDb.Id;
                    }

                    var detailsFromDb = await _orderServices.AddOrderDetailsAsync(orderDetailsDto);
                    if (detailsFromDb != null)
                    {
                        await _cartService.ClearCartAsync(userId);
                        TempData["success"] = "Commande créée avec succès";
                        return RedirectToAction("OrderConfirmation", "Order", new { orderId = orderHeaderFromDb.Id });
                    }
                    else
                    {
                        TempData["error"] = "Erreur lors de la création des détails de la commande";
                        return View(model);
                    }
                }
                else
                {
                    TempData["error"] = "Erreur lors de la création de l'en-tête de la commande";
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Une erreur s'est produite: {ex.Message} {(ex.InnerException != null ? ex.InnerException.Message : string.Empty)}";
                return View(model);
            }
        }
    }
}
            

     

            

