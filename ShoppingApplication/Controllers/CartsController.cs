using Application.DTO;
using Application.DTO.Cart;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

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

        [HttpGet]
        public ActionResult<CartDTO> GetCart()
        {
            var user = this.User.Identity as ClaimsIdentity;
            var userId = user.FindFirst(ClaimTypes.NameIdentifier).Value;
            var cart = _cartService.GetCart(userId);

            if (cart == null)
            {
                return NotFound();
            }

            return cart;
        }

        [HttpPost("{cartId}/products/add")]
        public ActionResult<ResultDTO> AddProductToCart(Guid cartId, [FromBody] AddCartProductDTO cartProduct)
        {
            var user = this.User.Identity as ClaimsIdentity;
            var userId = user.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = _cartService.AddProductToCart(userId, cartProduct);

            if (result == null)
            {
                return NotFound();
            }

            return result;
        }

        [HttpPost("create")]
        public ActionResult<ResultDTO> CreateCart([FromBody] Guid cartId)
        {
            var user = this.User.Identity as ClaimsIdentity;
            var userId = user.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = _cartService.CreateCart(userId, cartId);

            return result;
        }

        [HttpPost("{cartId}/products/delete")]
        public ActionResult<ResultDTO> DeleteProductFromCart(Guid cartId, [FromBody] Guid productId)
        {
            var user = this.User.Identity as ClaimsIdentity;
            var userId = user.FindFirst(ClaimTypes.NameIdentifier).Value;
            var result = _cartService.DeleteProductFromCart(userId, productId);

            if (result == null)
            {
                return NotFound();
            }

            return result;
        }
    }
}
