using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Discount
{
    public class Discount
    {
        public Discount(Guid id, string code, double amount)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("id cannot be empty");
            }

            if (string.IsNullOrEmpty(code))
            {
                throw new ArgumentException("code cannot be null or empty");
            }

            if (amount <= 0)
            {
                throw new ArgumentException("amount cannot be less than or equal to zero");
            }

            if (code.Length < 4 || code.Length > 50)
            {
                throw new ArgumentException("code must be greater than or equal to 4 characters and less than 50");
            }

            Id = id;
            Code = code;
            Amount = amount;
        }

        public Guid Id { get; }
        public string Code { get; }
        public double Amount { get; }
        public bool Active { get; set; }
    }
}
