using System.ComponentModel.DataAnnotations;

namespace Application_ECommerce.Models.Coupon
{
    public class CreateCouponViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string CouponCode { get; set; }

        [Required]
        [Range(0.01, 10000)]
        public decimal DiscountAmount { get; set; }

        [Required]
        [Range(0, 10000)]
        public decimal MinimumAmount { get; set; }
    }
}