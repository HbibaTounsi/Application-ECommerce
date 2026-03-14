using Application_ECommerce.Core.Common;
using Application_ECommerce.Core.Entities.Cart;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_ECommerce.Core.Entities.Product
{
    public class Product : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        [ForeignKey("Category")]
        public Guid CategoryId { get; set; }

        public Application_ECommerce.Core.Entities.Category.Category Category { get; set; }

        public ICollection<CartDetails> CartDetails { get; set; }

    }
}
