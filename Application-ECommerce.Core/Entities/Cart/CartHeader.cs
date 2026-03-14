using Application_ECommerce.Core.Common;
using Application_ECommerce.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_ECommerce.Core.Entities.Cart
{
    public class CartHeader : BaseEntity
    {
        [Required]
        [ForeignKey("user")]

        public string UserId { get; set; }


        [ForeignKey("Coupon")]
        public Guid? CouponId { get; set; }
        public string? CouponCode { get; set; }
        public ApplicationUser User { get; set; }
        public Application_ECommerce.Core.Entities.Coupon.Coupon Coupon { get; set; }
        public ICollection<CartDetails> CartDetails { get; set; }

    }
}
