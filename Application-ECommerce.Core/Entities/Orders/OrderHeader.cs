using Application_ECommerce.Core.Common;
using Application_ECommerce.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_ECommerce.Core.Entities.Orders
{
    public class OrderHeader : BaseEntity
    {
        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; }

        public string? CouponCode { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal Discount { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal? OrderTotal { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        [Required]
        public DateTime OrderTime { get; set; }
        public string? PaymentIntentId { get; set; }
        public string? StrioeSessionId { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
        public string Status { get; set; }

    }
}
