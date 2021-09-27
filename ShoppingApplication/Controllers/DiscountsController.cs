using Application.DTO;
using Application.DTO.Discount;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public ActionResult<DiscountDTO> GetDiscount(Guid id)
        {
            var discount = _discountService.GetDiscount(id);

            if (discount == null)
            {
                return NotFound();
            }

            return discount;
        }

        [HttpGet]
        public ActionResult<List<DiscountDTO>> GetDiscounts()
        {
            var discounts = _discountService.GetDiscounts();
            return discounts;
        }

        [HttpPost("create")]
        [Authorize(Policy = "Admin")]
        public ActionResult<ResultDTO> CreateDiscount([FromBody] CreateDiscountDTO createDiscountDTO)
        {
            var result = _discountService.CreateDiscount(createDiscountDTO);
            return result;
        }
    }
}
