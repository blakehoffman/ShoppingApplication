using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Order
{
    public class Product
    {
        public Product(Guid id, int price)
        {
            Id = id;
            Price = price;
        }

        public Guid Id { get; }
        public int Price { get; }
    }
}
