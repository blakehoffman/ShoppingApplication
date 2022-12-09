using System;

namespace Domain.Models.Order
{
    public class Product
    {
        public Product(Guid id, string name, decimal price, int quantity)
        {
            Id = id;
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public Guid Id { get; }
        public string Name { get; }
        public decimal Price { get; }
        public int Quantity { get; set; }
    }
}
