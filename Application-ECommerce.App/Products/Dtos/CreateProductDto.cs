using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Application_ECommerce.App.Products.Dtos
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public Guid CategoryID { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile? ImageFile { get; set; }

    }
}
