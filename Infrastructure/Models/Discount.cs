using System;

namespace Infrastructure.Models
{
    public class Discount
    {
        public Guid Id { get; set; }
        public int ClusterId { get; set; }
        public string Code { get; set; }
        public decimal Amount { get; set; }
        public bool Active { get; set; }
    }
}
