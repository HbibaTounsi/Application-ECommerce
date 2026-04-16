using Application_ECommerce.Core.Entities.Orders;
using Application_ECommerce.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application_ECommerce.Infrastructure.Persistence.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _context;

        public DashboardRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<decimal> GetTotalSalesAsync()
        {
            return await _context.OrderHeaders
                .Where(o => o.Status != "Cancelled") // Optional: filter by status
                .SumAsync(o => o.OrderTotal ?? 0);
        }

        public async Task<int> GetTotalOrdersCountAsync()
        {
            return await _context.OrderHeaders.CountAsync();
        }

        public async Task<int> GetTotalProductsCountAsync()
        {
            return await _context.Products.CountAsync();
        }

        public async Task<int> GetTotalCustomersCountAsync()
        {
            return await _context.ApplicationUsers.CountAsync();
        }

        public async Task<IEnumerable<OrderHeader>> GetRecentOrdersAsync(int count)
        {
            return await _context.OrderHeaders
                .OrderByDescending(o => o.OrderTime)
                .Take(count)
                .ToListAsync();
        }

        public async Task<IDictionary<string, decimal>> GetSalesByMonthAsync()
        {
            var sixMonthsAgo = DateTime.Now.AddMonths(-6);
            var salesData = await _context.OrderHeaders
                .Where(o => o.OrderTime >= sixMonthsAgo && o.Status != "Cancelled")
                .GroupBy(o => new { o.OrderTime.Year, o.OrderTime.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Total = g.Sum(o => o.OrderTotal ?? 0)
                })
                .OrderBy(x => x.Year)
                .ThenBy(x => x.Month)
                .ToListAsync();

            return salesData.ToDictionary(
                x => $"{x.Year}-{x.Month:D2}",
                x => x.Total
            );
        }

        public async Task<IDictionary<string, int>> GetOrdersByStatusAsync()
        {
            var stats = await _context.OrderHeaders
                .GroupBy(o => o.Status)
                .Select(g => new
                {
                    Status = g.Key ?? "Unknown",
                    Count = g.Count()
                })
                .ToListAsync();

            return stats.ToDictionary(x => x.Status, x => x.Count);
        }
    }
}
