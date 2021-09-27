using System;

namespace Infrastructure.Records
{
    public class CartRecord
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public bool Purchased { get; set; }
    }
}
