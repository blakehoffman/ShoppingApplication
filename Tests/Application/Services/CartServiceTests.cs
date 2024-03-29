﻿using Application.Services;
using AutoMapper;
using Domain.Models.Cart;
using Domain.Repositories;
using Moq;
using System;
using Application.DTO.Cart;
using Application.DTO;
using KellermanSoftware.CompareNetObjects;
using Xunit;
using Domain.UnitOfWork;
using ProductModel = Domain.Models.Product.Product;
using System.Threading.Tasks;

namespace Tests.Application.Services
{
    public class CartServiceTests
    {
        private readonly Mock<ICartRepository> _cartRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public CartServiceTests()
        {
            _cartRepositoryMock = new Mock<ICartRepository>();
            _mapperMock = new Mock<IMapper>();
            _productRepositoryMock = new Mock<IProductRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
        }

        [Fact]
        public async Task AddProductToCart()
        {
            var productID = Guid.NewGuid();

            _cartRepositoryMock.Setup(cartRepository => cartRepository.FindByUserId(It.IsAny<string>()))
                .ReturnsAsync(new Cart(Guid.NewGuid(), "UserId", DateTimeOffset.UtcNow));

            _productRepositoryMock.Setup(productRepository => productRepository.Find(It.IsAny<Guid>()))
                .ReturnsAsync(new ProductModel(productID, "test product", "description", Guid.NewGuid(), 3));

            var cartService = new CartService(_cartRepositoryMock.Object, _mapperMock.Object, _productRepositoryMock.Object, _unitOfWorkMock.Object);

            var cartProductDTO = new AddCartProductDTO
            {
                Id = productID,
                Quantity = 1
            };

            var expected = new ResultDTO { Succeeded = true };

            var actual = await cartService.AddProductToCart("user", Guid.NewGuid(), cartProductDTO);

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public async Task AddProductToCart_NoUser_Error()
        {
            var cartService = new CartService(_cartRepositoryMock.Object, _mapperMock.Object, _productRepositoryMock.Object, _unitOfWorkMock.Object);

            var cartProductDTO = new AddCartProductDTO();

            var expected = new ResultDTO
            {
                Succeeded = false,
                Errors = { "Can't add to a cart when user is not logged in" }
            };

            var actual = await cartService.AddProductToCart("", Guid.NewGuid(), cartProductDTO);

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public async Task AddProductToCart_QuantityLessThan1_Error()
        {
            var productID = Guid.NewGuid();

            _cartRepositoryMock.Setup(cartRepository => cartRepository.FindByUserId(It.IsAny<string>()))
                .ReturnsAsync(new Cart(Guid.NewGuid(), "UserId", DateTimeOffset.UtcNow));

            _productRepositoryMock.Setup(productRepository => productRepository.Find(It.IsAny<Guid>()))
                .ReturnsAsync(new ProductModel(productID, "test product", "description", Guid.NewGuid(), 3));

            var cartService = new CartService(_cartRepositoryMock.Object, _mapperMock.Object, _productRepositoryMock.Object, _unitOfWorkMock.Object);

            var cartProductDTO = new AddCartProductDTO
            {
                Id = productID,
                Quantity = 0
            };

            var expected = new ResultDTO
            {
                Succeeded = false,
                Errors = { "Quantity must be greater than 0" }
            };

            var actual = await cartService.AddProductToCart("user", Guid.NewGuid(), cartProductDTO);

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public async Task CreateCart()
        {
            _cartRepositoryMock.Setup(cartRepository => cartRepository.FindByUserId(It.IsAny<string>()))
                .ReturnsAsync((Cart)null);

            _cartRepositoryMock.Setup(cartRepository => cartRepository.Find(It.IsAny<Guid>()))
                .ReturnsAsync((Cart)null);

            var cartService = new CartService(_cartRepositoryMock.Object, _mapperMock.Object, _productRepositoryMock.Object, _unitOfWorkMock.Object);

            var expected = new ResultDTO { Succeeded = true };

            var actual = await cartService.CreateCart("User", Guid.NewGuid());

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public async Task CreateCart_DuplicateCartId_Error()
        {
            _cartRepositoryMock.Setup(cartRepository => cartRepository.FindByUserId(It.IsAny<string>()))
                .ReturnsAsync((Cart)null);

            _cartRepositoryMock.Setup(cartRepository => cartRepository.Find(It.IsAny<Guid>()))
                .ReturnsAsync(new Cart(Guid.NewGuid(), "UserId", DateTimeOffset.UtcNow));

            var cartService = new CartService(_cartRepositoryMock.Object, _mapperMock.Object, _productRepositoryMock.Object, _unitOfWorkMock.Object);

            var expected = new ResultDTO
            {
                Succeeded = false,
                Errors = { "A cart with this Id already exists" }
            };

            var actual = await cartService.CreateCart("User", Guid.NewGuid());

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public async Task CreateCart_NoUserError()
        {
            var cartService = new CartService(_cartRepositoryMock.Object, _mapperMock.Object, _productRepositoryMock.Object, _unitOfWorkMock.Object);

            var expected = new ResultDTO
            { 
                Succeeded = false,
                Errors = { "Can't create a cart when user is not logged in" }
            };

            var actual = await cartService.CreateCart("", Guid.NewGuid());

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public async Task CreateCart_UserAlreadyHasCart_Error()
        {
            _cartRepositoryMock.Setup(cartRepository => cartRepository.FindByUserId(It.IsAny<string>()))
                .ReturnsAsync(new Cart(Guid.NewGuid(), "UserId", DateTimeOffset.UtcNow));

            var cartService = new CartService(_cartRepositoryMock.Object, _mapperMock.Object, _productRepositoryMock.Object, _unitOfWorkMock.Object);

            var expected = new ResultDTO
            {
                Succeeded = false,
                Errors = { "Can't create a new cart when user already has one" }
            };

            var actual = await cartService.CreateCart("User", Guid.NewGuid());

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public async Task DeleteProductFromCart()
        {
            var productID = Guid.NewGuid();
            var userCart = new Cart(Guid.NewGuid(), "UserId", DateTimeOffset.UtcNow);
            userCart.AddItem(new Product(productID, "test product", 25, 1));

            _cartRepositoryMock.Setup(cartRepository => cartRepository.FindByUserId(It.IsAny<string>()))
                .ReturnsAsync(userCart);

            _productRepositoryMock.Setup(productRepository => productRepository.Find(It.IsAny<Guid>()))
                .ReturnsAsync(new ProductModel(productID, "test product", "description", Guid.NewGuid(), 25));

            var cartService = new CartService(_cartRepositoryMock.Object, _mapperMock.Object, _productRepositoryMock.Object, _unitOfWorkMock.Object);

            var expected = new ResultDTO { Succeeded = true };

            var actual = await cartService.DeleteProductFromCart("user", productID);

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public async Task DeleteProductFromCart_NoUser_Error()
        {
            var cartService = new CartService(_cartRepositoryMock.Object, _mapperMock.Object, _productRepositoryMock.Object, _unitOfWorkMock.Object);

            var expected = new ResultDTO
            {
                Succeeded = false,
                Errors = { "Can't edit a cart when user isn't signed in" }
            };

            var actual = await cartService.DeleteProductFromCart("", Guid.NewGuid());

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }
    }
}
