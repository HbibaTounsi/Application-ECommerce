using Application_ECommerce.App.Dashboard.Dtos;
using Application_ECommerce.App.Dashboard.Interfaces;
using Application_ECommerce.Core.Interfaces.Repositories;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Application_ECommerce.App.Dashboard
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardService(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public async Task<DashboardStatsDto> GetDashboardStatsAsync()
        {
            var stats = new DashboardStatsDto
            {
                TotalSales = await _dashboardRepository.GetTotalSalesAsync(),
                TotalOrders = await _dashboardRepository.GetTotalOrdersCountAsync(),
                TotalProducts = await _dashboardRepository.GetTotalProductsCountAsync(),
                TotalCustomers = await _dashboardRepository.GetTotalCustomersCountAsync(),
                RecentOrders = await _dashboardRepository.GetRecentOrdersAsync(5),
                SalesByMonth = await _dashboardRepository.GetSalesByMonthAsync(),
                OrdersByStatus = await _dashboardRepository.GetOrdersByStatusAsync()
            };

            return stats;
        }
    }
}
