using System;

namespace Domain.Models.Order
{
    public class Discount
    {

        public Discount(Guid id, decimal amount)
        {
            Id = id;
            Amount = amount;
        }

        public Guid Id { get; }
        public decimal Amount { get; }
    }
}
