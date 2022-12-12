using Application.DTO;
using Application.DTO.Product;
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
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(Guid id)
        {
            var product = await _productService.GetProduct(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> GetProducts(Guid? categoryId)
        {
            if (categoryId == null)
            {
                return await _productService.GetProducts();
            }
            else
            {
                return await _productService.GetProductsByCategory((Guid)categoryId);
            }
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<ResultDTO>> CreateProduct(CreateProductDTO createProductDTO)
        {
            var result = await _productService.CreateProduct(createProductDTO);
            return result;
        }
    }
}
