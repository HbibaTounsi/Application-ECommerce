using Application_ECommerce.Core.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_ECommerce.Core.Entities.Coupon
{
    public class Coupon : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string couponCode { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal DiscountAmount { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal MinimumAmount { get; set; }

    }
}
