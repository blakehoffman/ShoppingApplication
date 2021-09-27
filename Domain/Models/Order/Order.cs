using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Order
{
    public class Order
    {
        private readonly Discount? _discount;
        private IList<Product> _products;

        public Order(Guid id, string userId, DateTimeOffset dateTime, Discount? discount)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Id cannot be empty");
            }

            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentException("UserId cannot be empty");
            }

            Id = id;
            UserId = userId;
            DateTime = dateTime;
            _discount = discount;
            _products = new List<Product>();
        }

        public Order(Guid id, string userId, DateTimeOffset dateTime, Discount? discount, List<Product> products)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Id cannot be empty");
            }

            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentException("UserId cannot be empty");
            }

            Id = id;
            UserId = userId;
            DateTime = dateTime;
            _discount = discount;
            _products = products;
        }

        public Guid Id { get; }
        public string UserId { get; }
        public DateTimeOffset DateTime { get; }
        public Guid? Discount => _discount?.Id;
        public ImmutableList<Product> Products => _products.ToImmutableList();

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

        public double GetTotal()
        {
            var productTotal = Products.Sum(p => p.Price * p.Quantity);

            if (_discount != null)
            {
                return (double)productTotal * _discount.Amount;
            }

            return productTotal;
        }
    }
}
