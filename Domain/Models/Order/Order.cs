using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Order
{
    public class Order
    {
        private readonly double? _discount;

        public Order(Guid id, Guid userId, DateTimeOffset dateTime, double? discount, List<Product> products)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Id cannot be empty");
            }

            if (userId == Guid.Empty)
            {
                throw new ArgumentException("UserId cannot be empty");
            }

            Id = id;
            UserId = userId;
            DateTime = dateTime;
            _discount = discount;
            Products = products;
        }

        public Guid Id { get; }
        public Guid UserId { get; }
        public DateTimeOffset DateTime { get; }
        public List<Product> Products { get; } = new List<Product>();

        public double GetTotal()
        {
            var productTotal = Products.Sum(p => p.Price);

            if (_discount != null)
            {
                return productTotal * (double)_discount;
            }

            return productTotal;
        }
    }
}
