using Application_ECommerce.Core.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_ECommerce.Core.Entities.Cart
{
    public class CartDetails : BaseEntity
    {

        // ID de l'en-tête du panier (clé étrangère)
        [Required]
        [ForeignKey("CartHeader")]
        public Guid CartHeaderId { get; set; }

        // ID du produit ajouté au panier
        public Guid ProductId { get; set; }

        // Quantité du produit (entre 1 et 100)
        [Required]
        [Range(1, 100, ErrorMessage = "La quantité doit être entre 1 et 100.")]
        public int Count { get; set; }

        // Navigation vers le panier parent
        public CartHeader CartHeader { get; set; }
        public Application_ECommerce.Core.Entities.Product.Product Product { get; set; }

    }
}
