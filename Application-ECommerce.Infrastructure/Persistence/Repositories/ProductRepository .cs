using Application_ECommerce.Core.Entities.Product;
using Application_ECommerce.Core.Interfaces.Repositories;
using Application_ECommerce.Infrastructure.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_ECommerce.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : Repository<Product> , IProductRepository

    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Product> AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return (product);
        }
        public async Task<Product> ReadByIdAsync(Guid productId)
        {
            try
            {
                // recherche coupon
                var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId);
                if (product == null)
                {
                    throw new KeyNotFoundException($"Produit introuvable: {productId}");
                }
                return product;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erreur lors de la recherche du produit : {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<Product>> ReadAllAsync()
        {
            try
            {
                return await _context.Products.Include(p => p.Category).ToListAsync(); // pour inclure la catégorie
            }
            catch (Exception ex)
            {
                throw new Exception($"Erreur lors de la récupération des produits : {ex.Message}", ex);
            }
        }

         public async Task UpdateAsync(Product product)
         {
            try 
            {
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erreur lors de la mise à jour du produit : {ex.Message}", ex);
            }
         }
          public async Task DeleteAsync(Guid id)
          {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product != null)
                {
                    _context.Products.Remove(product);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erreur lors de la suppression du produit : {ex.Message}", ex);
            }
          }

        public async Task<IEnumerable<Product>> GetProductsByCategoryId(Guid categoryId)
        { 
            return await _context.Products
            .Where(p => p.CategoryId == categoryId)
            .Include(p => p.Category)
            .ToListAsync();
        }

    }
}
