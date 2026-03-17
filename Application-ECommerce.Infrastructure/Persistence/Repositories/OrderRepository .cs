using Application_ECommerce.Core.Entities.Orders;
using Application_ECommerce.Core.Interfaces.Repositories;
using Application_ECommerce.Core.Interfaces.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_ECommerce.Infrastructure.Persistence.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IRepository<OrderHeader> _orderHeaderRepo;
        private readonly IRepository<OrderDetails> _orderDetailsRepo;

        public  OrderRepository(
        ApplicationDbContext context,
        IRepository<OrderHeader> orderHeaderRepo,
        IRepository<OrderDetails> orderDetailsRepo)
    {
        _context = context;
        _orderHeaderRepo = orderHeaderRepo;
        _orderDetailsRepo = orderDetailsRepo;
    }

    //ORDER HEADER

    public async Task<OrderHeader> AddOrderHeaderAsync(OrderHeader orderHeader)
    {
        try
        {
            await _orderHeaderRepo.AddAsync(orderHeader);
            await _orderHeaderRepo.SaveChangesAsync();
            return orderHeader;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public IEnumerable<OrderHeader> GetAllAsync()
    {
        return _context.OrderHeaders.Include(o => o.OrderDetails)
        .ThenInclude(od => od.Product)
        .ToList();
    }

    public IEnumerable<OrderHeader> GetAllByUserIdAsync(string userId)
    {
        return _context.OrderHeaders.Include(o => o.OrderDetails)
        .ThenInclude(od => od.Product)
        .Where(o => o.UserId == userId)
        .ToList();
    }
    public async Task<OrderHeader?> GetByIdAsync(Guid orderHeaderId)
        {
            return await _context.OrderHeaders.FirstOrDefaultAsync(o => o.Id == orderHeaderId);
        }

    public async Task<OrderHeader?> GetByIdWithDetailsAsync(Guid orderHeaderId)
    {
        return await _context.OrderHeaders.Include(o => o.OrderDetails)
        .ThenInclude(od => od.Product)
        .FirstOrDefaultAsync(o => o.Id == orderHeaderId);
    }
        public async Task<OrderHeader?> UpdateOrderHeaderAsync(OrderHeader orderHeader)
        {
            try
            {
                _orderHeaderRepo.Update(orderHeader);
                await _orderHeaderRepo.SaveChangesAsync();
                return orderHeader;
            }

            catch (Exception ex)
            {
                throw new Exception($"Erreur lors de la mise à jour : {ex.Message}", ex);
            }
        }
                public async Task<bool> UpdateStatusAsync(Guid orderHeaderId, string newStatus)
                {
                    try
                    {
                        var order = await GetByIdAsync(orderHeaderId);
                        if (order == null) return false;

                        order.Status = newStatus;
                        _orderHeaderRepo.Update(order);
                        await _orderHeaderRepo.SaveChangesAsync();
                        return true;
                    }
                    catch 
                    {
                        return false;
                    }
                }
        //ORDER DETAILS
        public async Task<IEnumerable<OrderDetails>> AddOrderDetailsAsync(IEnumerable<OrderDetails> orderDetailsList)
        {
            try
            {
                await _context.OrderDetails.AddRangeAsync(orderDetailsList);
                await _context.SaveChangesAsync();
                return orderDetailsList;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erreur lors de l'ajout des OrderDetails : {ex.Message}", ex);
            }
        }
    }   


}
