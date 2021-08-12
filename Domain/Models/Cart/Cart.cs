using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Cart
{
    public class Cart
    {
        private List<Product> _products = new List<Product>();

        public Cart(Guid id, Guid userId, DateTimeOffset dateCreated)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException("id cannot empty");
            }

            if (userId == Guid.Empty)
            {
                throw new ArgumentNullException("userId cannot be empty");
            }

            Id = id;
            UserId = userId;
            DateCreated = dateCreated;
        }

        public Guid Id { get; }
        public Guid UserId { get; }
        public DateTimeOffset DateCreated { get; }
        public bool Purchased { get; private set; }
        public IReadOnlyCollection<Product> Products => _products.AsReadOnly();
        public int GetTotal => _products.Sum(p => p.Price * p.Quantity);

        public void AddItem(Product product)
        {
            if (product == null)
            {
                return;
            }

            var productInProducts = _products.Where(p => p.Id == product.Id).FirstOrDefault();

            if (productInProducts == null)
            {
                _products.Add(product);
            }
            else
            {
                productInProducts.Quantity++;
            }
        }

        public void RemoveItem(Product product)
        {
            if (product == null)
            {
                return;
            }

            var productToRemove = _products.Where(p => p.Id == product.Id).First();

            if (productToRemove != null)
            {
                _products.Remove(productToRemove);
            }
        }
    }
}
