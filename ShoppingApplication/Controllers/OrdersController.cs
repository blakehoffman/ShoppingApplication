using Application.DTO;
using Application.DTO.Order;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

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
        public List<OrderDTO> GetOrders()
        {
            return _orderService.GetOrders();
        }

        [HttpGet("by-user")]
        public List<OrderDTO> GetOrdersByUser()
        {
            var user = this.User.Identity as ClaimsIdentity;
            var userId = user.FindFirst(ClaimTypes.NameIdentifier).Value;

            return _orderService.GetOrders(userId);
        }

        [HttpPost("create")]
        public ActionResult<ResultDTO> CreateOrder([FromBody] CreateOrderDTO createOrderDTO)
        {
            var user = this.User.Identity as ClaimsIdentity;
            var userId = user.FindFirst(ClaimTypes.NameIdentifier).Value;

            return _orderService.CreateOrder(userId, createOrderDTO);
        }
    }
}
