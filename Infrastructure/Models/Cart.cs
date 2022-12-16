using System;
using System.Collections.Generic;

namespace Infrastructure.Models
{
    public class Cart
    {
        public Guid Id { get; set; }
        public int ClusterId { get; set; }
        public Guid UserId { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public bool Purchased { get; set; }
        public List<CartProduct> Products { get; set; }
    }
}
