using System;

namespace Infrastructure.Records
{
    public class OrderProductRecord
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
