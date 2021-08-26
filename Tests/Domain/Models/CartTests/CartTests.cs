using Domain.Models.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Domain.Models.CartTests
{
    public class CartTests
    {
        [Fact]
        public void AddItem()
        {
            var product = new Product(Guid.NewGuid(), "test name", 5, 1);
            var cart = new Cart(Guid.NewGuid(), Guid.NewGuid(), DateTimeOffset.Now);

            var expected = 1;

            cart.AddItem(product);

            Assert.True(cart.Products.Count == expected);
        }

        [Fact]
        public void AddItem_DuplicateProduct_NotAdded()
        {
            var product = new Product(Guid.NewGuid(), "test name", 5, 1);
            var productTwo = new Product(Guid.NewGuid(), "test name", 3, 1);
            var cart = new Cart(Guid.NewGuid(), Guid.NewGuid(), DateTimeOffset.Now);
            cart.AddItem(product);
            cart.AddItem(productTwo);

            var expectedProductCount = 2;
            var expectedProductQuantity = 2;

            cart.AddItem(product);

            Assert.True(cart.Products.Count == expectedProductCount);
            Assert.True(cart.Products.ElementAt(0).Quantity == expectedProductQuantity);
        }

        [Fact]
        public void AddItem_NullProduct_NotAdded()
        {
            Product product = null;
            var cart = new Cart(Guid.NewGuid(), Guid.NewGuid(), DateTimeOffset.Now);

            var expected = 0;

            cart.AddItem(product);

            Assert.True(cart.Products.Count == expected);
        }

        [Fact]
        public void RemoveItem()
        {
            Product product = new Product(Guid.NewGuid(), "test name", 5, 1);
            var cart = new Cart(Guid.NewGuid(), Guid.NewGuid(), DateTimeOffset.Now);
            cart.AddItem(product);

            var expected = 0;

            cart.RemoveItem(product);

            Assert.True(cart.Products.Count == expected);
        }

        [Fact]
        public void RemoveItem_NullProduct_NoChange()
        {
            Product nullProduct = null;
            Product product = new Product(Guid.NewGuid(), "test name", 5, 1);
            var cart = new Cart(Guid.NewGuid(), Guid.NewGuid(), DateTimeOffset.Now);
            cart.AddItem(product);

            var expected = 1;

            cart.RemoveItem(nullProduct);

            Assert.True(cart.Products.Count == expected);
        }

    }
}
