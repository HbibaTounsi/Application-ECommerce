using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_ECommerce.App.Orders.Dtos
{
    public class OrderDetailsDto
    {
        public Guid Id { get; set; }
        public Guid OrderHeaderId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductUrl { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
    }
}