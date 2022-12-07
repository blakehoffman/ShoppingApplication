using System;

namespace Infrastructure.Records
{
    public class DiscountRecord
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public decimal Amount { get; set; }
        public bool Active { get; set; }
    }
}
