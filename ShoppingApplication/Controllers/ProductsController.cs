using Application.DTO;
using Application.DTO.Product;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

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
        public ActionResult<ProductDTO> GetProduct(Guid id)
        {
            var product = _productService.GetProduct(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        [HttpGet]
        public ActionResult<List<ProductDTO>> GetProducts(Guid? categoryId)
        {
            if (categoryId == null)
            {
                return _productService.GetProducts();
            }
            else
            {
                return _productService.GetProductsByCategory((Guid)categoryId);
            }
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public ActionResult<ResultDTO> CreateProduct(CreateProductDTO createProductDTO)
        {
            var result = _productService.CreateProduct(createProductDTO);
            return result;
        }
    }
}
