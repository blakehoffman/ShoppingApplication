using Application.DTO;
using Application.DTO.Discount;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : Controller
    {
        private readonly IDiscountService _discountService;

        public DiscountsController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DiscountDTO>> GetDiscount(Guid id)
        {
            var discount = await _discountService.GetDiscount(id);

            if (discount == null)
            {
                return NotFound();
            }

            return discount;
        }

        [HttpGet]
        public async Task<ActionResult<List<DiscountDTO>>> GetDiscounts()
        {
            var discounts = await _discountService.GetDiscounts();
            return discounts;
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<ResultDTO>> CreateDiscount([FromBody] CreateDiscountDTO createDiscountDTO)
        {
            var result = await _discountService.CreateDiscount(createDiscountDTO);
            return result;
        }
    }
}
