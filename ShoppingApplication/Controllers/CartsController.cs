using Application.DTO;
using Application.DTO.Cart;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CartsController : Controller
    {
        private readonly ICartService _cartService;

        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("me")]
        public async Task<ActionResult<CartDTO>> GetCart()
        {
            var user = this.User.Identity as ClaimsIdentity;
            var userId = user.FindFirst(ClaimTypes.NameIdentifier).Value;
            var cart = await _cartService.GetCart(userId);

            if (cart == null)
            {
                return NotFound();
            }

            return cart;
        }

        [HttpPut("{cartId}/products/{productId}")]
        public async Task<ActionResult<ResultDTO>> AddProductToCart(Guid cartId, Guid productId, [FromBody] AddCartProductDTO cartProduct)
        {
            if (cartProduct.Id != null && cartProduct.Id != productId)
            {
                return BadRequest();
            }

            var user = this.User.Identity as ClaimsIdentity;
            var userId = user.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = await _cartService.AddProductToCart(userId, productId, cartProduct);

            if (result == null)
            {
                return NotFound();
            }

            return result;
        }

        [HttpPost]
        public async Task<ActionResult<ResultDTO>> CreateCart([FromBody] Guid cartId)
        {
            var user = this.User.Identity as ClaimsIdentity;
            var userId = user.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = await _cartService.CreateCart(userId, cartId);

            return result;
        }

        [HttpDelete("{cartId}/products/{productId}")]
        public async Task<ActionResult<ResultDTO>> DeleteProductFromCart(Guid cartId, Guid productId)
        {
            var user = this.User.Identity as ClaimsIdentity;
            var userId = user.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = await _cartService.DeleteProductFromCart(userId, productId);

            if (result == null)
            {
                return NotFound();
            }

            return result;
        }
    }
}
