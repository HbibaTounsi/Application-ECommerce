using Application_ECommerce.Core.Entities.Product;
using Application_ECommerce.Core.Interfaces.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_ECommerce.Core.Interfaces.Repositories
{
    public interface IProductRepository : IRepository<Application_ECommerce.Core.Entities.Product.Product>

    {
        // Ajoute un nouveau produit à la base de données
        Task<Product> AddAsync(Product product);

        // Récupère un produit par son identifiant
        Task<Product> ReadByIdAsync(Guid productId);

        // Récupère tous les produits existants
        Task<IEnumerable<Product>> ReadAllAsync();

        // Met à jour un produit existant
        Task UpdateAsync(Product product);

        // Supprime un produit par son identifiant et retourne true si l'opération a réussi
        Task DeleteAsync(Guid productId);

        Task<IEnumerable<Product>> GetProductsByCategoryId(Guid categoryId);

    }
}
