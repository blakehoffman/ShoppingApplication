using Application.DTO;
using Application.DTO.Cart;
using System;

namespace Application.Services.Interfaces
{
    public interface ICartService
    {
        public ResultDTO AddProductToCart(string userId, CartProductDTO productDTO);
        public ResultDTO CreateCart(string userId, Guid cartId);
        public ResultDTO DeleteProductFromCart(string userId, Guid cartId);
        public CartDTO GetCart(string userId);
        public ResultDTO UpdateProductQuantityInCart(string userId, UpdateProductQuantityInCartDTO updateProductQuantityDTO);
    }
}
