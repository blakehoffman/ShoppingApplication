using System;

namespace Infrastructure.Records
{
    public class OrderRecord
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set;}
        public DateTimeOffset OrderDate { get; set; }
        public Guid? DiscountId { get; set;}
    }
}
