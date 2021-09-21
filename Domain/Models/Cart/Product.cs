using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Cart
{
    public class Product
    {
        public Product(Guid id, string name, int price, int quantity)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException("id cannot be empty");
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name cannot be null or empty");
            }

            Id = id;
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public Guid Id { get; }
        public string Name { get; }
        public int Price { get; }
        public int Quantity { get; private set; }

        public void UpdateQuantity(int quantity)
        {
            if (quantity > 0)
            {
                Quantity = quantity;
            }
        }
    }
}
