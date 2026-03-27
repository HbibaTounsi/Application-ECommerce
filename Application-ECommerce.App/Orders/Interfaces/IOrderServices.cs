using Application_ECommerce.App.Orders.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_ECommerce.App.Orders.Interfaces
{
     public interface IOrderServices
    {
        Task<OrderHeaderDto?> AddOrderHeaderAsync(OrderHeaderDto orderHeaderDto);
        Task<IEnumerable<OrderDetailsDto>?> AddOrderDetailsAsync(IEnumerable<OrderDetailsDto> orderDetailsDtoList);

        IEnumerable<OrderHeaderDto> GetAllOrdersAsync();
        IEnumerable<OrderHeaderDto> GetOrdersByUserIdAsync(string userId);
        Task<OrderHeaderDto?> GetOrderByIdAsync(Guid orderHeaderId);
        Task<OrderHeaderDto?> GetOrderWithDetailsByIdAsync(Guid orderHeaderId);

        Task<bool?> UpdateOrderStatusAsync(Guid orderHeaderId, string newStatus);
        Task<OrderHeaderDto?> UpdateOrderHeaderAsync(OrderHeaderDto orderHeaderDto);
    }
}