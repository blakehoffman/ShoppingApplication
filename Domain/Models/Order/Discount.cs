using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Order
{
    public class Discount
    {

        public Discount(Guid id, double amount)
        {
            Id = id;
            Amount = amount;
        }

        public Guid Id { get; }
        public double Amount { get; }
    }
}
