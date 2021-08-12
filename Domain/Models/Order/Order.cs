using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Order
{
    public class Order
    {
        private readonly double? _discount;
        private List<Product> _products = new List<Product>();

        public Order(Guid id, Guid userId, DateTimeOffset dateTime, double? discount)
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
        }

        public Guid Id { get; }
        public Guid UserId { get; }
        public DateTimeOffset DateTime { get; }
        public ReadOnlyCollection<Product> Products => _products.AsReadOnly();

        public void AddProduct(Product product)
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

        public double GetTotal()
        {
            var productTotal = Products.Sum(p => p.Price * p.Quantity);

            if (_discount != null)
            {
                return productTotal * (double)_discount;
            }

            return productTotal;
        }
    }
}
