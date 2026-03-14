using Application_ECommerce.Core.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_ECommerce.Core.Entities.Category
{
    public class Category : BaseEntity
    {
        [Required(ErrorMessage = "The category name is required")]
        [MaxLength(100, ErrorMessage = "The category name cannot exceed 100 characters")]
        public string Name { get; set; }

        [MaxLength(500, ErrorMessage = "The description name cannot exceed 500 characters")]
        public string Description { get; set; }

        public ICollection<Application_ECommerce.Core.Entities.Product.Product> Products { get; set; }

    }
}
