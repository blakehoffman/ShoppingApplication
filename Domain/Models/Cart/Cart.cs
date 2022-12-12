using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Domain.Models.Cart
{
    public class Cart
    {
        private IList<Product> _products;

        public Cart(Guid id, string userId, DateTimeOffset dateCreated)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException("id cannot empty");
            }

            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException("userId cannot be empty");
            }

            Id = id;
            UserId = userId;
            DateCreated = dateCreated;
            _products = new List<Product>();
        }

        public Cart(Guid id, string userId, DateTimeOffset dateCreated, List<Product> products)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException("id cannot empty");
            }

            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException("userId cannot be empty");
            }

            Id = id;
            UserId = userId;
            DateCreated = dateCreated;
            _products = products;
        }

        public Guid Id { get; }

        public string UserId { get; }

        public DateTimeOffset DateCreated { get; }

        public bool Purchased { get; set; }

        public ImmutableList<Product> Products => _products.ToImmutableList();

        public decimal GetTotal => _products.Sum(p => p.Price * p.Quantity);

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
                productInProducts.UpdateQuantity(productInProducts.Quantity + 1);
            }
        }

        public void AddProducts(IEnumerable<Product> products)
        {
            if (products == null)
            {
                return;
            }

            _products.Clear();
            
            foreach (var product in products)
            {
                AddItem(product);
            }
        }

        public void RemoveItem(Guid productId)
        {
            if (productId == Guid.Empty)
            {
                return;
            }

            var productToRemove = _products.Where(p => p.Id == productId).FirstOrDefault();

            if (productToRemove != null)
            {
                _products.Remove(productToRemove);
            }
        }

        public void UpdateProductQuantity(Guid productId, int quantity)
        {
            var product = Products.Where(product => product.Id == productId).FirstOrDefault();

            if (product == null)
            {
                return;
            }

            product.UpdateQuantity(quantity);
        }
    }
}
