using Application.DTO;
using Application.DTO.Product;
using Application.Services;
using AutoMapper;
using Domain.Models.Product;
using Domain.Repositories;
using KellermanSoftware.CompareNetObjects;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Application.Services
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        public ProductServiceTests()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _mapperMock = new Mock<IMapper>();
        }

        [Fact]
        public async Task CreateProduct_DuplicateName_Error()
        {
            _productRepositoryMock.Setup(productRepository => productRepository.FindByName(It.IsAny<string>()))
                .ReturnsAsync(new Product(Guid.NewGuid(), "Test Name", "test description", Guid.NewGuid(), 5));

            var productService = new ProductService(_productRepositoryMock.Object, _mapperMock.Object);

            var createProductDTO = new CreateProductDTO
            {
                Name = "Test name",
                Description = "Test description",
                CategoryId = Guid.NewGuid(),
                Price = 3
            };

            var expected = new ResultDTO
            {
                Succeeded = false,
                Errors = { "A product with this name already exists" }
            };

            var actual = await productService.CreateProduct(createProductDTO);

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public async Task CreateProduct_EmptyName_Error()
        {
            var productService = new ProductService(_productRepositoryMock.Object, _mapperMock.Object);

            var createProductDTO = new CreateProductDTO
            {
                Name = "",
                Description = "Test description",
                CategoryId = Guid.NewGuid(),
                Price = 3
            };

            var expected = new ResultDTO
            {
                Succeeded = false,
                Errors = { "Product name cannot be empty and must be between 4 and 50 characters" }
            };

            var actual = await productService.CreateProduct(createProductDTO);

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public async Task CreateProduct_NameGreaterThan50Characters_Error()
        {
            var productService = new ProductService(_productRepositoryMock.Object, _mapperMock.Object);

            var createProductDTO = new CreateProductDTO
            {
                Name = new string('a', 51),
                Description = "Test description",
                CategoryId = Guid.NewGuid(),
                Price = 3
            };

            var expected = new ResultDTO
            {
                Succeeded = false,
                Errors = { "Product name cannot be empty and must be between 4 and 50 characters" }
            };

            var actual = await productService.CreateProduct(createProductDTO);

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public async Task CreateProduct_NameLessThan4Characters()
        {
            var productService = new ProductService(_productRepositoryMock.Object, _mapperMock.Object);

            var createProductDTO = new CreateProductDTO
            {
                Name = "tes",
                Description = "Test description",
                CategoryId = Guid.NewGuid(),
                Price = 3
            };

            var expected = new ResultDTO
            {
                Succeeded = false,
                Errors = { "Product name cannot be empty and must be between 4 and 50 characters" }
            };

            var actual = await productService.CreateProduct(createProductDTO);

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public async Task CreateProduct_EmptyDescription_Error()
        {
            var productService = new ProductService(_productRepositoryMock.Object, _mapperMock.Object);

            var createProductDTO = new CreateProductDTO
            {
                Name = "Test name",
                Description = "",
                CategoryId = Guid.NewGuid(),
                Price = 3
            };

            var expected = new ResultDTO
            {
                Succeeded = false,
                Errors = { "Product description cannot be empty and must be between 8 and 100 characters" }
            };

            var actual = await productService.CreateProduct(createProductDTO);

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public async Task CreateProduct_DescriptionLessThan8Characters_Error()
        {
            var productService = new ProductService(_productRepositoryMock.Object, _mapperMock.Object);

            var createProductDTO = new CreateProductDTO
            {
                Name = "Test name",
                Description = "test",
                CategoryId = Guid.NewGuid(),
                Price = 3
            };

            var expected = new ResultDTO
            {
                Succeeded = false,
                Errors = { "Product description cannot be empty and must be between 8 and 100 characters" }
            };

            var actual = await productService.CreateProduct(createProductDTO);

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public async Task CreateProduct_DescriptionGreaterThan100Characters_Error()
        {
            var productService = new ProductService(_productRepositoryMock.Object, _mapperMock.Object);

            var createProductDTO = new CreateProductDTO
            {
                Name = "Test name",
                Description = new string('a', 101),
                CategoryId = Guid.NewGuid(),
                Price = 3
            };

            var expected = new ResultDTO
            {
                Succeeded = false,
                Errors = { "Product description cannot be empty and must be between 8 and 100 characters" }
            };

            var actual = await productService.CreateProduct(createProductDTO);

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public async Task CreateProduct_EmptyProductId_Error()
        {
            var productService = new ProductService(_productRepositoryMock.Object, _mapperMock.Object);

            var createProductDTO = new CreateProductDTO
            {
                Name = "Test name",
                Description = "Test description",
                CategoryId = Guid.Empty,
                Price = 3
            };

            var expected = new ResultDTO
            {
                Succeeded = false,
                Errors = { "Product category id cannot be empty" }
            };

            var actual = await productService.CreateProduct(createProductDTO);

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public async Task CreateProduct_PriceIsZero_Error()
        {
            var productService = new ProductService(_productRepositoryMock.Object, _mapperMock.Object);

            var createProductDTO = new CreateProductDTO
            {
                Name = "Test name",
                Description = "Test description",
                CategoryId = Guid.NewGuid(),
                Price = 0
            };

            var expected = new ResultDTO
            {
                Succeeded = false,
                Errors = { "Product price must be above 0" }
            };

            var actual = await productService.CreateProduct(createProductDTO);

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }
    }
}
