using System;

namespace Infrastructure.Models
{
    public class CartProduct
    {
        public Guid CartId { get; set; }
        public int Quantity { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
