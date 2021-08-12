using Domain.Models.Order;
using System;
using System.Collections.Generic;
using Xunit;

namespace Tests.Domain.Models.OrderTests
{
    public class OrderTest
    {
        [Fact]
        public void AddProduct()
        {
            var product = new Product(Guid.NewGuid(), 5, 1);
            var order = new Order(Guid.NewGuid(), Guid.NewGuid(), DateTimeOffset.Now, null);

            var expected = 1;

            order.AddProduct(product);

            Assert.True(order.Products.Count == expected);
        }

        [Fact]
        public void AddProduct_DuplicateProduct()
        {
            var product = new Product(Guid.NewGuid(), 5, 1);
            var productTwo = new Product(Guid.NewGuid(), 3, 1);
            var order = new Order(Guid.NewGuid(), Guid.NewGuid(), DateTimeOffset.Now, null);
            order.AddProduct(product);
            order.AddProduct(productTwo);
            
            var expectedProductCount = 2;
            var expectedProductQuantity = 2;

            order.AddProduct(product);

            Assert.True(order.Products.Count == expectedProductCount);
            Assert.True(order.Products[0].Quantity == expectedProductQuantity);
        }

        [Fact]
        public void AddProduct_NullProduct()
        {
            Product product = null;
            var order = new Order(Guid.NewGuid(), Guid.NewGuid(), DateTimeOffset.Now, null);

            var expected = 0;

            order.AddProduct(product);

            Assert.True(order.Products.Count == expected);
        }

        [Fact]
        public void GetTotal()
        {
            var product = new Product(Guid.NewGuid(), 5, 1);
            var productTwo = new Product(Guid.NewGuid(), 3, 1);
            var order = new Order(Guid.NewGuid(), Guid.NewGuid(), DateTimeOffset.Now, null);
            order.AddProduct(product);
            order.AddProduct(productTwo);

            var expected = 8;

            var actual = order.GetTotal();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetTotal_WithDiscount()
        {
            var product = new Product(Guid.NewGuid(), 10, 1);
            var productTwo = new Product(Guid.NewGuid(), 10, 1);
            var order = new Order(Guid.NewGuid(), Guid.NewGuid(), DateTimeOffset.Now, 0.25);
            order.AddProduct(product);
            order.AddProduct(productTwo);

            var expected = 5;

            var actual = order.GetTotal();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetTotal_WithProductQuantityTwo()
        {
            var product = new Product(Guid.NewGuid(), 5, 1);
            var productTwo = new Product(Guid.NewGuid(), 3, 2);
            var order = new Order(Guid.NewGuid(), Guid.NewGuid(), DateTimeOffset.Now, null);
            order.AddProduct(product);
            order.AddProduct(productTwo);

            var expected = 11;

            var actual = order.GetTotal();

            Assert.Equal(expected, actual);
        }
    }
}
