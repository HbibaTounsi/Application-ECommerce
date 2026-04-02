using Microsoft.AspNetCore.Mvc;

namespace Application_ECommerce.Controllers
{
    public class CouponController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
