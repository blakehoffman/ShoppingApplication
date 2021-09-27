using Application.DTO;
using Application.DTO.Cart;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Models.Cart;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public CartService(
            ICartRepository cartRepository,
            IMapper mapper,
            IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public ResultDTO? AddProductToCart(string userId, CartProductDTO productDTO)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return new ResultDTO
                {
                    Succeeded = false,
                    Errors = { "Can't add to a cart when user is not logged in" }
                };
            }

            var userCart = _cartRepository.FindByUserId(userId);

            if (userCart == null)
            {
                return null;
            }

            var resultDTO = new ResultDTO();

            if (productDTO.Quantity == 0)
            {
                resultDTO.Errors.Add("Quantity must be greater than 0");
            }

            var product = _productRepository.Find(productDTO.Id);

            if (product == null)
            {
                resultDTO.Errors.Add("Product not found");
            }

            if (resultDTO.Errors.Count > 0)
            {
                resultDTO.Succeeded = false;
                return resultDTO;
            }

            userCart.AddItem(new Product(product.Id, product.Name, product.Price, productDTO.Quantity));
            _cartRepository.Update(userCart);
            resultDTO.Succeeded = true;

            return resultDTO;
        }

        public ResultDTO CreateCart(string userId, Guid cartGuid)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return new ResultDTO
                {
                    Succeeded = false,
                    Errors = { "Can't create a cart when user is not logged in" }
                };
            }

            var usersExistingCart = _cartRepository.FindByUserId(userId);

            if (usersExistingCart != null)
            {
                return new ResultDTO
                {
                    Succeeded = false,
                    Errors = { "Can't create a new cart when user already has one" }
                };
            }

            var existingCartWithId = _cartRepository.Find(cartGuid);

            if (existingCartWithId != null)
            {
                return new ResultDTO
                {
                    Succeeded = false,
                    Errors = { "A cart with this Id already exists" }
                };
            }

            var cart = new Cart(cartGuid, userId, DateTimeOffset.UtcNow);
            _cartRepository.Add(cart);

            return new ResultDTO { Succeeded = true };
        }

        public ResultDTO? DeleteProductFromCart(string userId, Guid productId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return new ResultDTO
                {
                    Succeeded = false,
                    Errors = { "Can't edit a cart when user isn't signed in" }
                };
            }

            var userCart = _cartRepository.FindByUserId(userId);

            if (userCart == null)
            {
                return null;
            }

            var product = _productRepository.Find(productId);

            if (product == null)
            {
                return new ResultDTO
                {
                    Succeeded = false,
                    Errors = { "Product not found" }
                };
            }

            userCart.RemoveItem(productId);
            _cartRepository.Update(userCart);

            return new ResultDTO { Succeeded = true };
        }

        public CartDTO? GetCart(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return null;
            }

            var cart = _cartRepository.FindByUserId(userId);
            return _mapper.Map<CartDTO>(cart);
        }

        public ResultDTO? UpdateProductQuantityInCart(string userId, UpdateProductQuantityInCartDTO updateProductQuantityDTO)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return new ResultDTO
                {
                    Succeeded = false,
                    Errors = { "Can't edit a cart when user isn't signed in" }
                };
            }

            var userCart = _cartRepository.FindByUserId(userId);

            if (userCart == null)
            {
                return null;
            }

            var product = _productRepository.Find(updateProductQuantityDTO.ProductId);

            if (product == null)
            {
                return new ResultDTO
                {
                    Succeeded = false,
                    Errors = { "Product not found" }
                };
            }

            userCart.UpdateProductQuantity(updateProductQuantityDTO.ProductId, updateProductQuantityDTO.Quantity);
            _cartRepository.Update(userCart);

            return new ResultDTO { Succeeded = true };
        }
    }
}
