using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application_ECommerce.Core.Entities.Orders;

namespace Application_ECommerce.Core.Interfaces.Repositories
{
    public interface IDashboardRepository
    {
        Task<decimal> GetTotalSalesAsync();
        Task<int> GetTotalOrdersCountAsync();
        Task<int> GetTotalProductsCountAsync();
        Task<int> GetTotalCustomersCountAsync();
        Task<IEnumerable<OrderHeader>> GetRecentOrdersAsync(int count);
        Task<IDictionary<string, decimal>> GetSalesByMonthAsync();
        Task<IDictionary<string, int>> GetOrdersByStatusAsync();
    }
}
