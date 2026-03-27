using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_ECommerce.App.Coupons.Dtos
{
     public class UpdateCouponDto
    {
        public Guid Id { get; set; }
        public string CouponCode { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal MinimumAmount { get; set; }
    }
}