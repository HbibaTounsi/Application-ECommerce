using Application_ECommerce.Core.Entities.Orders;
using System.Collections.Generic;

namespace Application_ECommerce.App.Dashboard.Dtos
{
    public class DashboardStatsDto
    {
        public decimal TotalSales { get; set; }
        public int TotalOrders { get; set; }
        public int TotalProducts { get; set; }
        public int TotalCustomers { get; set; }
        public IEnumerable<OrderHeader> RecentOrders { get; set; }
        public IDictionary<string, decimal> SalesByMonth { get; set; }
        public IDictionary<string, int> OrdersByStatus { get; set; }
    }
}
