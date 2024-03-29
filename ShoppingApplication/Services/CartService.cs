﻿using Application.DTO;
using Application.DTO.Cart;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Models.Cart;
using Domain.Repositories;
using Domain.UnitOfWork;
using System;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CartService(
            ICartRepository cartRepository,
            IMapper mapper,
            IProductRepository productRepository,
            IUnitOfWork unitOfWork)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultDTO> AddProductToCart(string userId, Guid productId, AddCartProductDTO addCartProductDTO)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return new ResultDTO
                {
                    Succeeded = false,
                    Errors = { "Can't add to a cart when user is not logged in" }
                };
            }

            var userCart = await _cartRepository.FindByUserId(userId);

            if (userCart == null)
            {
                return null;
            }

            var product = await _productRepository.Find(productId);

            if (product == null)
            {
                return null;
            }

            var resultDTO = new ResultDTO();

            if (addCartProductDTO.Quantity == 0)
            {
                resultDTO.Errors.Add("Quantity must be greater than 0");
            }

            if (resultDTO.Errors.Count > 0)
            {
                resultDTO.Succeeded = false;
                return resultDTO;
            }

            userCart.AddItem(new Product(productId, product.Name, product.Price, addCartProductDTO.Quantity));
            
            try
            {
                _unitOfWork.Begin();
                await _cartRepository.Update(userCart);
                _unitOfWork.Commit();

                resultDTO.Succeeded = true;
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                resultDTO.Succeeded = false;
            }

            return resultDTO;
        }

        public async Task<ResultDTO> CreateCart(string userId, Guid cartGuid)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return new ResultDTO
                {
                    Succeeded = false,
                    Errors = { "Can't create a cart when user is not logged in" }
                };
            }

            var usersExistingCart = await _cartRepository.FindByUserId(userId);

            if (usersExistingCart != null)
            {
                return new ResultDTO
                {
                    Succeeded = false,
                    Errors = { "Can't create a new cart when user already has one" }
                };
            }

            var existingCartWithId = await _cartRepository.Find(cartGuid);

            if (existingCartWithId != null)
            {
                return new ResultDTO
                {
                    Succeeded = false,
                    Errors = { "A cart with this Id already exists" }
                };
            }

            var cart = new Cart(cartGuid, userId, DateTimeOffset.UtcNow);
            await _cartRepository.Add(cart);

            return new ResultDTO { Succeeded = true };
        }

        public async Task<ResultDTO> DeleteProductFromCart(string userId, Guid productId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return new ResultDTO
                {
                    Succeeded = false,
                    Errors = { "Can't edit a cart when user isn't signed in" }
                };
            }

            var userCart = await _cartRepository.FindByUserId(userId);

            if (userCart == null)
            {
                return null;
            }

            var product = _productRepository.Find(productId);

            if (product == null)
            {
                return null;
            }

            userCart.RemoveItem(productId);
            await _cartRepository.Update(userCart);

            return new ResultDTO { Succeeded = true };
        }

        public async Task<CartDTO> GetCart(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return null;
            }

            var cart = await _cartRepository.FindByUserId(userId);
            return _mapper.Map<CartDTO>(cart);
        }
    }
}
