using Microsoft.AspNetCore.Mvc;

namespace Application_ECommerce.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
