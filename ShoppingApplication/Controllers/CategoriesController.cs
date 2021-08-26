using Application.DTO;
using Application.DTO.Category;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public ActionResult<List<CategoryDTO>> GetCategories(Guid? parentId)
        {
            var categories = _categoryService.GetCategories(parentId);
            return categories;
        }

        [HttpGet("{id}")]
        public ActionResult<CategoryDTO> GetCategory(Guid id)
        {
            var category = _categoryService.GetCategory(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        [HttpPost("create")]
        public ActionResult<ResultDTO> CreateCategory([FromBody] CreateCategoryDTO createCategoryDTO)
        {
            var result = _categoryService.CreateCategory(createCategoryDTO);
            return result;
        }
    }
}
