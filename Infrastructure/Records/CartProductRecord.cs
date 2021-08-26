using System;

namespace Infrastructure.Records
{
    public class CartProductRecord
    {
        public Guid CartId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
