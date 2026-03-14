using Application_ECommerce.Core.Entities.Category;
using Application_ECommerce.Core.Interfaces.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_ECommerce.Core.Interfaces.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {


        // Récupère une catégorie par son identifiant
        Task<Category> ReadByIdAsync(Guid categoryId);

        // Récupère toutes les catégories existantes
        Task<IEnumerable<Category>> ReadAllAsync();

        // Trouve l'identifiant d'une catégorie à partir de son nom
        Task<Guid?> GetCategoryIdByCategoryNameAsync(string categoryName);

        // Supprime une catégorie par son identifiant
        Task DeleteAsync(Guid categoryId);

    }
}
