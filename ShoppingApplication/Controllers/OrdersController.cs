using Application.DTO;
using Application.DTO.Order;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        [Authorize(Policy = "Admin")]
        public async Task<List<OrderDTO>> GetOrders()
        {
            return await _orderService.GetOrders();
        }

        [HttpGet("me")]
        public async Task<List<OrderDTO>> GetOrdersByUser()
        {
            var user = this.User.Identity as ClaimsIdentity;
            var userId = user.FindFirst(ClaimTypes.NameIdentifier).Value;

            return await _orderService.GetOrders(userId);
        }

        [HttpPost]
        public async Task<ActionResult<ResultDTO>> CreateOrder([FromBody] CreateOrderDTO createOrderDTO)
        {
            var user = this.User.Identity as ClaimsIdentity;
            var userId = user.FindFirst(ClaimTypes.NameIdentifier).Value;

            return await _orderService.CreateOrder(userId, createOrderDTO);
        }
    }
}
