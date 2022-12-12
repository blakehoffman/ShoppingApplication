using System;

namespace Domain.Models.Product
{
    public class Product
    {
        public Product(Guid id, string name, string description, Guid categoryId, decimal price)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException("Guid cannot be empty");
            }

            if (name == null)
            {
                throw new ArgumentNullException("Name cannot be null");
            }

            if (description == null)
            {
                throw new ArgumentNullException("Description cannot be nulil");
            }

            if (categoryId == Guid.Empty)
            {
                throw new ArgumentNullException("Category ID cannot be empty");
            }

            if (name.Length < 4 || name.Length > 50)
            {
                throw new ArgumentException("Name must be between 4 and 50 characters");
            }

            if (description.Length < 8 || description.Length > 100)
            {
                throw new ArgumentException("Description must be between 8 and 100 characters");
            }

            if (price <= 0)
            {
                throw new ArgumentException("Price must be greater than zero");
            }

            Id = id;
            Name = name;
            Description = description;
            CategoryId = categoryId;
            Price = price;
        }

        public Guid Id { get; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Guid CategoryId { get; }

        public decimal Price { get; private set; }

        public void ChangePrice(decimal newPrice)
        {
            if (newPrice <= 0)
            {
                return;
            }

            Price = newPrice;
        }
    }
}
