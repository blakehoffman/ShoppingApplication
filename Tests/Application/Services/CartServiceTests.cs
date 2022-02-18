using Application.Services;
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
        public void AddProductToCart()
        {
            var productID = Guid.NewGuid();

            _cartRepositoryMock.Setup(cartRepository => cartRepository.FindByUserId(It.IsAny<string>()))
                .Returns(new Cart(Guid.NewGuid(), "UserId", DateTimeOffset.UtcNow));

            _productRepositoryMock.Setup(productRepository => productRepository.Find(It.IsAny<Guid>()))
                .Returns(new ProductModel(productID, "test product", "description", Guid.NewGuid(), 3));

            var cartService = new CartService(_cartRepositoryMock.Object, _mapperMock.Object, _productRepositoryMock.Object, _unitOfWorkMock.Object);

            var cartProductDTO = new AddCartProductDTO
            {
                Id = productID,
                Quantity = 1
            };

            var expected = new ResultDTO { Succeeded = true };

            var actual = cartService.AddProductToCart("user", cartProductDTO);

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public void AddProductToCart_NoUser_Error()
        {
            var cartService = new CartService(_cartRepositoryMock.Object, _mapperMock.Object, _productRepositoryMock.Object, _unitOfWorkMock.Object);

            var cartProductDTO = new AddCartProductDTO();

            var expected = new ResultDTO
            {
                Succeeded = false,
                Errors = { "Can't add to a cart when user is not logged in" }
            };

            var actual = cartService.AddProductToCart("", cartProductDTO);

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public void AddProductToCart_NoneExistentProduct_Error()
        {
            _cartRepositoryMock.Setup(cartRepository => cartRepository.FindByUserId(It.IsAny<string>()))
                .Returns(new Cart(Guid.NewGuid(), "UserId", DateTimeOffset.UtcNow));

            _productRepositoryMock.Setup(productRepository => productRepository.Find(It.IsAny<Guid>()))
                .Returns((ProductModel)null);

            var cartService = new CartService(_cartRepositoryMock.Object, _mapperMock.Object, _productRepositoryMock.Object, _unitOfWorkMock.Object);

            var cartProductDTO = new AddCartProductDTO
            {
                Id = Guid.NewGuid(),
                Quantity = 1
            };

            var expected = new ResultDTO
            {
                Succeeded = false,
                Errors = { "Product not found" }
            };

            var actual = cartService.AddProductToCart("user", cartProductDTO);

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public void AddProductToCart_QuantityLessThan1_Error()
        {
            var productID = Guid.NewGuid();

            _cartRepositoryMock.Setup(cartRepository => cartRepository.FindByUserId(It.IsAny<string>()))
                .Returns(new Cart(Guid.NewGuid(), "UserId", DateTimeOffset.UtcNow));

            _productRepositoryMock.Setup(productRepository => productRepository.Find(It.IsAny<Guid>()))
                .Returns(new ProductModel(productID, "test product", "description", Guid.NewGuid(), 3));

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

            var actual = cartService.AddProductToCart("user", cartProductDTO);

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public void CreateCart()
        {
            _cartRepositoryMock.Setup(cartRepository => cartRepository.FindByUserId(It.IsAny<string>()))
                .Returns((Cart)null);

            _cartRepositoryMock.Setup(cartRepository => cartRepository.Find(It.IsAny<Guid>()))
                .Returns((Cart)null);

            var cartService = new CartService(_cartRepositoryMock.Object, _mapperMock.Object, _productRepositoryMock.Object, _unitOfWorkMock.Object);

            var expected = new ResultDTO { Succeeded = true };

            var actual = cartService.CreateCart("User", Guid.NewGuid());

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public void CreateCart_DuplicateCartId_Error()
        {
            _cartRepositoryMock.Setup(cartRepository => cartRepository.FindByUserId(It.IsAny<string>()))
                .Returns((Cart)null);

            _cartRepositoryMock.Setup(cartRepository => cartRepository.Find(It.IsAny<Guid>()))
                .Returns(new Cart(Guid.NewGuid(), "UserId", DateTimeOffset.UtcNow));

            var cartService = new CartService(_cartRepositoryMock.Object, _mapperMock.Object, _productRepositoryMock.Object, _unitOfWorkMock.Object);

            var expected = new ResultDTO
            {
                Succeeded = false,
                Errors = { "A cart with this Id already exists" }
            };

            var actual = cartService.CreateCart("User", Guid.NewGuid());

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public void CreateCart_NoUserError()
        {
            var cartService = new CartService(_cartRepositoryMock.Object, _mapperMock.Object, _productRepositoryMock.Object, _unitOfWorkMock.Object);

            var expected = new ResultDTO
            { 
                Succeeded = false,
                Errors = { "Can't create a cart when user is not logged in" }
            };

            var actual = cartService.CreateCart("", Guid.NewGuid());

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public void CreateCart_UserAlreadyHasCart_Error()
        {
            _cartRepositoryMock.Setup(cartRepository => cartRepository.FindByUserId(It.IsAny<string>()))
                .Returns(new Cart(Guid.NewGuid(), "UserId", DateTimeOffset.UtcNow));

            var cartService = new CartService(_cartRepositoryMock.Object, _mapperMock.Object, _productRepositoryMock.Object, _unitOfWorkMock.Object);

            var expected = new ResultDTO
            {
                Succeeded = false,
                Errors = { "Can't create a new cart when user already has one" }
            };

            var actual = cartService.CreateCart("User", Guid.NewGuid());

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public void DeleteProductFromCart()
        {
            var productID = Guid.NewGuid();
            var userCart = new Cart(Guid.NewGuid(), "UserId", DateTimeOffset.UtcNow);
            userCart.AddItem(new Product(productID, "test product", 25, 1));

            _cartRepositoryMock.Setup(cartRepository => cartRepository.FindByUserId(It.IsAny<string>()))
                .Returns(userCart);

            _productRepositoryMock.Setup(productRepository => productRepository.Find(It.IsAny<Guid>()))
                .Returns(new ProductModel(productID, "test product", "description", Guid.NewGuid(), 25));

            var cartService = new CartService(_cartRepositoryMock.Object, _mapperMock.Object, _productRepositoryMock.Object, _unitOfWorkMock.Object);

            var expected = new ResultDTO { Succeeded = true };

            var actual = cartService.DeleteProductFromCart("user", productID);

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public void DeleteProductFromCart_NonExistentProduct_Error()
        {
            _cartRepositoryMock.Setup(cartRepository => cartRepository.FindByUserId(It.IsAny<string>()))
                .Returns(new Cart(Guid.NewGuid(), "UserId", DateTimeOffset.UtcNow));

            _productRepositoryMock.Setup(productRepository => productRepository.Find(It.IsAny<Guid>()))
                .Returns((ProductModel)null);

            var cartService = new CartService(_cartRepositoryMock.Object, _mapperMock.Object, _productRepositoryMock.Object, _unitOfWorkMock.Object);

            var expected = new ResultDTO
            {
                Succeeded = false,
                Errors = { "Product not found" }
            };

            var actual = cartService.DeleteProductFromCart("user", Guid.NewGuid());

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public void DeleteProductFromCart_NoUser_Error()
        {
            var cartService = new CartService(_cartRepositoryMock.Object, _mapperMock.Object, _productRepositoryMock.Object, _unitOfWorkMock.Object);

            var expected = new ResultDTO
            {
                Succeeded = false,
                Errors = { "Can't edit a cart when user isn't signed in" }
            };

            var actual = cartService.DeleteProductFromCart("", Guid.NewGuid());

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public void UpdateProductQuantityInCart()
        {
            var productID = Guid.NewGuid();

            _cartRepositoryMock.Setup(cartRepository => cartRepository.FindByUserId(It.IsAny<string>()))
                .Returns(new Cart(Guid.NewGuid(), "UserId", DateTimeOffset.UtcNow));

            _productRepositoryMock.Setup(productRepository => productRepository.Find(It.IsAny<Guid>()))
                .Returns(new ProductModel(Guid.NewGuid(), "test product", "description", Guid.NewGuid(), 25));

            var cartService = new CartService(_cartRepositoryMock.Object, _mapperMock.Object, _productRepositoryMock.Object, _unitOfWorkMock.Object);

            var updateProductQuantityInCartDTO = new UpdateProductQuantityInCartDTO
            {
                ProductId = productID,
                Quantity = 50,
            };

            var expected = new ResultDTO { Succeeded = true };

            var actual = cartService.UpdateProductQuantityInCart("User", updateProductQuantityInCartDTO);

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public void UpdateProductQuantityInCart_NoUser_Error()
        {
            var cartService = new CartService(_cartRepositoryMock.Object, _mapperMock.Object, _productRepositoryMock.Object, _unitOfWorkMock.Object);

            var expected = new ResultDTO
            {
                Succeeded = false,
                Errors = { "Can't edit a cart when user isn't signed in" }
            };

            var actual = cartService.UpdateProductQuantityInCart("", new UpdateProductQuantityInCartDTO());

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }

        [Fact]
        public void UpdateProductQuantityInCart_NonExistentProduct_Error()
        {
            var productID = Guid.NewGuid();

            _cartRepositoryMock.Setup(cartRepository => cartRepository.FindByUserId(It.IsAny<string>()))
                .Returns(new Cart(Guid.NewGuid(), "UserId", DateTimeOffset.UtcNow));

            _productRepositoryMock.Setup(productRepository => productRepository.Find(It.IsAny<Guid>()))
                .Returns((ProductModel)null);

            var cartService = new CartService(_cartRepositoryMock.Object, _mapperMock.Object, _productRepositoryMock.Object, _unitOfWorkMock.Object);

            var expected = new ResultDTO
            {
                Succeeded = false,
                Errors = { "Product not found" }
            };

            var actual = cartService.UpdateProductQuantityInCart("User", new UpdateProductQuantityInCartDTO());

            var compareLogic = new CompareLogic();
            var result = compareLogic.Compare(expected, actual);
            Assert.True(result.AreEqual);
        }
    }
}
