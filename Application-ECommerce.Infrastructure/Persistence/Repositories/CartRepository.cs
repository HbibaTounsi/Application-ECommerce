using Application_ECommerce.Core.Entities.Cart;
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
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IRepository<CartHeader> _cartHeaderRepo;
        private readonly IRepository<CartDetails> _cartDetailsRepo;

        public CartRepository(
            ApplicationDbContext context,
            IRepository<CartHeader> cartHeaderRepo,
            IRepository<CartDetails> cartDetailsRepo)
        {
            _context = context;
            _cartHeaderRepo = cartHeaderRepo;
            _cartDetailsRepo = cartDetailsRepo;
        }

        // ---------------------------------------------
        // CART HEADER
        // ---------------------------------------------

        public async Task<CartHeader?> GetCartHeaderByUserIdAsync(string userId)
        {
            return await _context.CartHeaders
                .Include(ch => ch.CartDetails)
                    .ThenInclude(cd => cd.Product)
                .FirstOrDefaultAsync(ch => ch.UserId == userId);
        }

        public async Task<CartHeader?> GetCartHeaderByCartHeaderIdAsync(Guid cartHeaderId)
        {
            return await _context.CartHeaders
                .Include(ch => ch.CartDetails)
                    .ThenInclude(cd => cd.Product)
                .FirstOrDefaultAsync(ch => ch.Id == cartHeaderId);
        }

        public async Task<CartHeader> AddCartHeaderAsync(CartHeader cartHeader)
        {
            await _cartHeaderRepo.AddAsync(cartHeader);
            await _cartHeaderRepo.SaveChangesAsync();
            return cartHeader;
        }

        public async Task<CartHeader?> UpdateCartHeaderAsync(CartHeader cartHeader)
        {
            _cartHeaderRepo.Update(cartHeader);
            await _cartHeaderRepo.SaveChangesAsync();
            return cartHeader;
        }

        public async Task<CartHeader?> RemoveCartHeaderAsync(CartHeader cartHeader)
        {
            _cartHeaderRepo.Remove(cartHeader);
            await _cartHeaderRepo.SaveChangesAsync();
            return cartHeader;
        }

        // ---------------------------------------------
        // CART DETAILS
        // ---------------------------------------------

        public async Task<IEnumerable<CartDetails>> GetListCartDetailsByCartHeaderIdAsync(Guid cartHeaderId)
        {
            return await _context.CartDetails
                .Include(cd => cd.Product)
                .Where(cd => cd.CartHeaderId == cartHeaderId)
                .ToListAsync();
        }

        public async Task<CartDetails?> GetCartDetailsByCartHeaderIdAndProductId(Guid cartHeaderId, Guid productId)
        {
            return await _context.CartDetails
                .FirstOrDefaultAsync(cd =>
                    cd.CartHeaderId == cartHeaderId &&
                    cd.ProductId == productId);
        }

        public async Task<CartDetails?> GetCartDetailsByCartDetailsId(Guid cartDetailsId)
        {
            return await _cartDetailsRepo.GetByIdAsync(cartDetailsId);
        }

        public async Task<CartDetails> AddCartDetailsAsync(CartDetails cartDetails)
        {
            await _cartDetailsRepo.AddAsync(cartDetails);
            await _cartDetailsRepo.SaveChangesAsync();
            return cartDetails;
        }

        public async Task<CartDetails?> UpdateCartDetailsAsync(CartDetails cartDetails)
        {
            _cartDetailsRepo.Update(cartDetails);
            await _cartDetailsRepo.SaveChangesAsync();
            return cartDetails;
        }

        public async Task<CartDetails?> RemoveCartDetailsAsync(CartDetails cartDetails)
        {
            _cartDetailsRepo.Remove(cartDetails);
            await _cartDetailsRepo.SaveChangesAsync();
            return cartDetails;
        }

        // ---------------------------------------------
        // FUNCTIONS ADDITIONNELLES
        // ---------------------------------------------

        public async Task<bool> ClearCartAsync(string userId)
        {
            var header = await GetCartHeaderByUserIdAsync(userId);

            if (header == null) return false;

            foreach (var detail in header.CartDetails)
                _cartDetailsRepo.Remove(detail);

            _cartHeaderRepo.Remove(header);

            await _cartHeaderRepo.SaveChangesAsync();
            return true;
        }

        public async Task<int> TotalCountofCartItemAsync(Guid cartHeaderId)
        {
            return await _context.CartDetails
                .Where(cd => cd.CartHeaderId == cartHeaderId)
                .SumAsync(cd => cd.Count);
        }
    }
}