using Application.DTO;
using Application.DTO.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface ICartService
    {
        public ResultDTO? AddProductToCart(string userId, CartProductDTO productDTO);
        public ResultDTO CreateCart(string userId, Guid cartId);
        public ResultDTO? DeleteProductFromCart(string userId, Guid cartId);
        public CartDTO? GetCart(string userId);
        public ResultDTO? UpdateProductQuantityInCart(string userId, UpdateProductQuantityInCartDTO updateProductQuantityDTO);
    }
}
