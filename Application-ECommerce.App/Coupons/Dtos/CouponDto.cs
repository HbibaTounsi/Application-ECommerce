using System;


namespace Application_ECommerce.App.Coupons.Dtos
{
    public class CouponDto
    {
        public Guid Id { get; set; }
        public string CouponCode { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal MinimumAmount { get; set; }
    }
}