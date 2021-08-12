using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Domain.Models.Product;

namespace Tests.Domain.Models.ProductTests
{
    public class ProductTest
    {
        [Fact]
        public void ChangePrice()
        {
            var product = new Product(Guid.NewGuid(), "Test product", "Test description", Guid.NewGuid(), 5);

            var expected = 10;

            product.ChangePrice(10);

            Assert.True(product.Price == expected);
        }

        [Fact]
        public void ChangePrice_ToZero()
        {
            var product = new Product(Guid.NewGuid(), "Test product", "Test description", Guid.NewGuid(), 5);

            var expected = 5;

            product.ChangePrice(0);

            Assert.True(product.Price == expected);
        }
    }
}
