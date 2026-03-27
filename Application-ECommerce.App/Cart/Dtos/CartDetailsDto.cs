using Application_ECommerce.App.Products.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_ECommerce.App.Cart.Dtos
{
     public class CartDetailsDto
    {
        public Guid Id { get; set; }
        public Guid CartHeaderId { get; set; }
        public Guid ProductId { get; set; }
        public int Count { get; set; }

        // For UI display purposes
        public ProductDto? Product { get; set; }

        // Additional calculated properties for UI
        public decimal? Price { get; set; }
    }
}