using Application_ECommerce.App.Dashboard.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Application_ECommerce.Controllers
{
    // [Authorize(Roles = "Admin")] // Décommenter après avoir configuré les rôles si nécessaire
    public class AdminController : Controller
    {
        private readonly IDashboardService _dashboardService;

        public AdminController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public async Task<IActionResult> Index()
        {
            var stats = await _dashboardService.GetDashboardStatsAsync();
            return View(stats);
        }

        [HttpGet]
        public async Task<IActionResult> GetSalesData()
        {
            var stats = await _dashboardService.GetDashboardStatsAsync();
            return Json(new { salesByMonth = stats.SalesByMonth, ordersByStatus = stats.OrdersByStatus });
        }
    }
}
