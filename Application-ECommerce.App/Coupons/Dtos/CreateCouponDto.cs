using System.ComponentModel.DataAnnotations;

namespace Application_ECommerce.App.Coupons.Dtos
{
    public class CreateCouponDto
    {
        [Required]
        [MaxLength(50)]
        public string CouponCode { get; set; }

        [Required]
        public decimal DiscountAmount { get; set; }

        [Required]
        public decimal MinimumAmount { get; set; }
    }
}