using Application_ECommerce.Core.Entities.Coupon;
using Application_ECommerce.Core.Interfaces.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_ECommerce.Core.Interfaces.Repositories
{
    public interface ICouponRepository : IRepository<Coupon>
    {
        // Ajoute un nouveau coupon à la base de données
        Task<Coupon> AddAsync(Coupon coupon);

        // Récupère un coupon par son identifiant
        Task<Coupon> ReadByIdAsync(Guid couponId);

        // Récupère un coupon par son code de réduction
        Task<Coupon> ReadByCouponCodeAsync(string couponCode);

        // Récupère tous les coupons existants
        Task<IEnumerable<Coupon>> ReadAllAsync();

        // Met à jour un coupon existant
        Task UpdateAsync(Coupon coupon);

        // Supprime un coupon par son identifiant
        Task DeleteAsync(Guid id);

    }
}
