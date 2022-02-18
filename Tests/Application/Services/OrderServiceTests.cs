using Application.DTO;
using Application.DTO.Order;
using Application.Services;
using AutoMapper;
using Domain.Models.Cart;
using Domain.Repositories;
using Domain.UnitOfWork;
using KellermanSoftware.CompareNetObjects;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using DiscountModel = Domain.Models.Discount.Discount;
using ProductModel = Domain.Models.Product.Product;

namespace Tests.Application.Services
{
    public class OrderServiceTests
    {
        private readonly Mock<ICartRepository> _cartRepositoryMock;
        private readonly Mock<IDiscountRepository> _discountRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IOrderRepository> _orderRepositoryMock;
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public OrderServiceTests()
        {
            _cartRepositoryMock = new Mock<ICartRepository>();
            _discountRepositoryMock = new Mock<IDiscountRepository>();
            _mapperMock = new Mock<IMapper>();
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _productRepositoryMock = new Mock<IProductRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Fact]
        public void CreateOrder()
        {
            _discountRepositoryMock.Setup(discountRepository => discountRepository.FindByCode(It.IsAny<string>()))
                .Returns(new DiscountModel(Guid.NewGuid(), "test", 0.25));

            _productRepositoryMock.Setup(productRepository => productRepository.Find(It.IsAny<Guid>()))
                .Returns(new ProductModel(Guid.NewGuid(), "test", "description", Guid.NewGuid(), 25));

            _cartRepositoryMock.Setup(cartRepository => cartRepository.FindByUserId(It.IsAny<string>()))
                .Returns((Cart)null);

            var orderService = new OrderService(
                _cartRepositoryMock.Object,
                _discountRepositoryMock.Object,
                _mapperMock.Object,
                _orderRepositoryMock.Object,
                _productRepositoryMock.Object,
                _unitOfWorkMock.Object);

            var createOrderDTO = new CreateOrderDTO
            {
                Products = new List<CreateOrderProductDTO>
                {
                    new CreateOrderProductDTO { Id = Guid.NewGuid(), Quantity = 1 }
                },
                Discount = null
            };

            var expected = new ResultDTO { Succeeded = true };

            var actual = orderService.CreateOrder("user", createOrderDTO);

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public void CreateOrder_InvalidDiscount_Error()
        {
            var productID = Guid.NewGuid();

            _discountRepositoryMock.Setup(discountRepository => discountRepository.FindByCode(It.IsAny<string>()))
                .Returns(new DiscountModel(Guid.NewGuid(), "test", 0.25));

            _productRepositoryMock.Setup(productRepository => productRepository.Find(It.IsAny<Guid>()))
                .Returns((ProductModel)null);

            var orderService = new OrderService(
                _cartRepositoryMock.Object,
                _discountRepositoryMock.Object,
                _mapperMock.Object,
                _orderRepositoryMock.Object,
                _productRepositoryMock.Object,
                _unitOfWorkMock.Object);

            var createOrderDTO = new CreateOrderDTO
            {
                Products = new List<CreateOrderProductDTO>
                {
                    new CreateOrderProductDTO { Id = productID, Quantity = 1 }
                },
            };

            var expected = new ResultDTO
            {
                Succeeded = false,
                Errors = new List<string> { $"Product {productID} does not exist" }
            };

            var actual = orderService.CreateOrder("user", createOrderDTO);

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public void CreateOrder_InvalidProduct_Error()
        {
            _discountRepositoryMock.Setup(discountRepository => discountRepository.FindByCode(It.IsAny<string>()))
                .Returns((DiscountModel)null);

            _productRepositoryMock.Setup(productRepository => productRepository.Find(It.IsAny<Guid>()))
                .Returns(new ProductModel(Guid.NewGuid(), "test", "description", Guid.NewGuid(), 25));

            var orderService = new OrderService(
                _cartRepositoryMock.Object,
                _discountRepositoryMock.Object,
                _mapperMock.Object,
                _orderRepositoryMock.Object,
                _productRepositoryMock.Object,
                _unitOfWorkMock.Object);

            var createOrderDTO = new CreateOrderDTO
            {
                Products = new List<CreateOrderProductDTO>
                {
                    new CreateOrderProductDTO { Id = Guid.NewGuid(), Quantity = 1 }
                },
                Discount = "test"
            };

            var expected = new ResultDTO
            {
                Succeeded = false,
                Errors = new List<string> { "Discount doesn't exist" }
            };

            var actual = orderService.CreateOrder("user", createOrderDTO);

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public void CreateOrder_NullUsername_Error()
        {

            _productRepositoryMock.Setup(productRepository => productRepository.Find(It.IsAny<Guid>()))
                .Returns(new ProductModel(Guid.NewGuid(), "test", "description", Guid.NewGuid(), 25));

            var orderService = new OrderService(
                _cartRepositoryMock.Object,
                _discountRepositoryMock.Object,
                _mapperMock.Object,
                _orderRepositoryMock.Object,
                _productRepositoryMock.Object,
                _unitOfWorkMock.Object);

            var expected = new ResultDTO
            {
                Succeeded = false,
                Errors = new List<string> { "User ID cannot be empty" }
            };

            var actual = orderService.CreateOrder(null, new CreateOrderDTO
            {
                Products = new List<CreateOrderProductDTO>
                {
                    new CreateOrderProductDTO { Id = Guid.NewGuid(), Quantity = 1 }
                }
            });

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public void CreateOrder_ZeroProducts_Error()
        {
            var orderService = new OrderService(
                _cartRepositoryMock.Object,
                _discountRepositoryMock.Object,
                _mapperMock.Object,
                _orderRepositoryMock.Object,
                _productRepositoryMock.Object,
                _unitOfWorkMock.Object);

            var expected = new ResultDTO
            {
                Succeeded = false,
                Errors = new List<string> { "In order to create an order, there must be at least one product attached to it" }
            };

            var actual = orderService.CreateOrder("test", new CreateOrderDTO());

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }
    }
}
