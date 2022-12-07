using Domain.Models.Order;
using System;
using System.Linq;
using Xunit;

namespace Tests.Domain.Models.OrderTests
{
    public class OrderTest
    {
        [Fact]
        public void AddItem()
        {
            var product = new Product(Guid.NewGuid(), "test name", 5, 1);
            var order = new Order(Guid.NewGuid(), "user", DateTimeOffset.UtcNow, null);

            var expected = 1;

            order.AddItem(product);

            Assert.True(order.Products.Count == expected);
        }

        [Fact]
        public void AddItem_DuplicateProduct_NotAdded()
        {
            var product = new Product(Guid.NewGuid(), "test name", 5, 1);
            var productTwo = new Product(Guid.NewGuid(), "test name", 3, 1);
            var order = new Order(Guid.NewGuid(), "user", DateTimeOffset.UtcNow, null);
            order.AddItem(product);
            order.AddItem(productTwo);
            
            var expectedProductCount = 2;
            var expectedProductQuantity = 2;

            order.AddItem(product);

            Assert.True(order.Products.Count == expectedProductCount);
            Assert.True(order.Products.ElementAt(0).Quantity == expectedProductQuantity);
        }

        [Fact]
        public void AddItem_NullProduct_NotAdded()
        {
            Product product = null;
            var order = new Order(Guid.NewGuid(), "user", DateTimeOffset.UtcNow, null);

            var expected = 0;

            order.AddItem(product);

            Assert.True(order.Products.Count == expected);
        }

        [Fact]
        public void GetTotal()
        {
            var product = new Product(Guid.NewGuid(), "test name", 5, 1);
            var productTwo = new Product(Guid.NewGuid(), "test name", 3, 1);
            var order = new Order(Guid.NewGuid(), "user", DateTimeOffset.UtcNow, null);
            order.AddItem(product);
            order.AddItem(productTwo);

            var expected = 8;

            var actual = order.GetTotal();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetTotal_WithDiscount()
        {
            var product = new Product(Guid.NewGuid(), "test name", 10, 1);
            var productTwo = new Product(Guid.NewGuid(), "test name", 10, 1);
            var order = new Order(Guid.NewGuid(), "user", DateTimeOffset.UtcNow, new Discount(Guid.NewGuid(), 0.25m));
            order.AddItem(product);
            order.AddItem(productTwo);

            var expected = 5;

            var actual = order.GetTotal();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetTotal_WithProductQuantityTwo()
        {
            var product = new Product(Guid.NewGuid(), "test name", 5, 1);
            var productTwo = new Product(Guid.NewGuid(), "test name", 3, 2);
            var order = new Order(Guid.NewGuid(), "user", DateTimeOffset.UtcNow, null);
            order.AddItem(product);
            order.AddItem(productTwo);

            var expected = 11;

            var actual = order.GetTotal();

            Assert.Equal(expected, actual);
        }
    }
}
