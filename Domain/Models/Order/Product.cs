using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Order
{
    public class Product
    {
        public Product(Guid id, int price, int quantity)
        {
            Id = id;
            Price = price;
            Quantity = quantity;
        }

        public Guid Id { get; }
        public int Price { get; }
        public int Quantity { get; set; }
    }
}
