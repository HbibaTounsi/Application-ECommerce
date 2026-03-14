using Application_ECommerce.Core.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_ECommerce.Core.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        // OrderHeader
        Task<OrderHeader> AddOrderHeaderAsync(OrderHeader orderHeader);
        Task<OrderHeader?> GetByIdAsync(Guid orderHeaderId);
        Task<OrderHeader?> GetByIdWithDetailsAsync(Guid orderHeaderId);
        IEnumerable<OrderHeader> GetAllAsync();
        IEnumerable<OrderHeader> GetAllByUserIdAsync(string userId);
        Task<OrderHeader?> UpdateOrderHeaderAsync(OrderHeader orderHeader);
        Task<bool> UpdateStatusAsync(Guid orderHeaderId, string newStatus);

        // OrderDetails
        Task<IEnumerable<OrderDetails>> AddOrderDetailsAsync(IEnumerable<OrderDetails> orderDetailsList);

    }
}
