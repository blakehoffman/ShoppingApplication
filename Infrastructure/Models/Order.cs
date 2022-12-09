using System;
using System.Collections.Generic;

namespace Infrastructure.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public int ClusterId { get; set; }
        public Guid UserId { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public Guid? DiscountId { get; set; }
        public Discount Discount { get; set; }
        public List<OrderProduct> Products { get; set; }
    }
}
