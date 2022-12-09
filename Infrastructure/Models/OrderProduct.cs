using System;

namespace Infrastructure.Models
{
    public class OrderProduct
    {
        public int ClusterId { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
