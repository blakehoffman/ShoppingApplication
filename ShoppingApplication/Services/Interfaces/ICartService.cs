using Application.DTO;
using Application.DTO.Cart;
using System;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface ICartService
    {
        public Task<ResultDTO> AddProductToCart(string userId, Guid productId, AddCartProductDTO productDTO);
        public ResultDTO CreateCart(string userId, Guid cartId);
        public ResultDTO DeleteProductFromCart(string userId, Guid cartId);
        public CartDTO GetCart(string userId);
    }
}
