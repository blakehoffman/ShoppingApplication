using Application.DTO;
using Application.DTO.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface ICategoryService
    {
        public ResultDTO CreateCategory(CreateCategoryDTO categoryDTO);
        public List<CategoryDTO> GetCategories(Guid? parentId);
        public CategoryDTO? GetCategory(Guid id);
    }
}
