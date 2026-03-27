using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_ECommerce.App.Categories.Dtos
{
    public class UpdateCategoryDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Category name is required")]
        [StringLength(100, ErrorMessage = "The Category name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Category description is required")]
        [StringLength(500, ErrorMessage = "The description cannot exceed 500 characters.")]
        public string Description { get; set; }
    }
}
