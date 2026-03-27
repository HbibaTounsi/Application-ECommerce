using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_ECommerce.App.Cart.Dtos
{
     public class CartHeaderDto
    {
        public Guid Id { get; set; }
        public string UserID { get; set; }
        public string? CouponCode { get; set; }

        // For UI/calculation
        public decimal? CartTotal { get; set; }
        public decimal? Discount { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
    }
}