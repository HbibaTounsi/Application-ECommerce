using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_ECommerce.Core.Interfaces.Repositories.Base

{ // Interface générique pour les opérations CRUD (Create, Read, Update, Delete)
    // Pattern Repository: sépare la logique métier de l'accès aux données
    public interface IRepository<TEntity> where TEntity : class
    {
        //ajouter une nouvelle entité et retourne l'entité avec son id générer
        Task<TEntity> AddAsync(TEntity entity);
        //récupérer une nouvelle entité et retourne l'entité avec ID générer
        Task<TEntity> GetByIdAsync(Guid id);

        // Récupère toutes les entités (attention: peut retourner beaucoup de données)
        Task<IEnumerable<TEntity>> GetAllAsync();

        // Met à jour une entité existante
        Task Update(TEntity entity);

        // Supprime une entité
        Task Remove(TEntity entity);

        // Supprime une entité
        Task<int> SaveChangesAsync();

    }
}
