using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Order
{
    public class Product
    {
        public Product(Guid id, string name, int price, int quantity)
        {
            Id = id;
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public Guid Id { get; }
        public string Name { get; }
        public int Price { get; }
        public int Quantity { get; set; }
    }
}
