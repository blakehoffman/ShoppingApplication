using Application.DTO;
using Application.DTO.Category;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Models.Categories;
using Domain.Repositories;
using System;
using System.Collections.Generic;

namespace Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public ResultDTO CreateCategory(CreateCategoryDTO categoryDTO)
        {
            var resultDTO = new ResultDTO();

            if (string.IsNullOrEmpty(categoryDTO.Name) || categoryDTO.Name.Length < 3 || categoryDTO.Name.Length > 100)
            {
                resultDTO.Errors.Add("Category name cannot be empty and between 3 and 100 characters");
            }

            if (categoryDTO.ParentId != null)
            {
                var parentCategory = _categoryRepository.Find((Guid)categoryDTO.ParentId);

                if (parentCategory == null)
                {
                    resultDTO.Errors.Add("Parent category doesn't exist");
                }
            }

            if (_categoryRepository.FindByName(categoryDTO.Name) != null)
            {
                resultDTO.Errors.Add("A category with this name already exists");
            }

            if (resultDTO.Errors.Count > 0)
            {
                resultDTO.Succeeded = false;
                return resultDTO;
            }

            var category = new Category(Guid.NewGuid(), categoryDTO.Name, categoryDTO.ParentId);
            _categoryRepository.Add(category);
            resultDTO.Succeeded = true;

            return resultDTO;
        }

        public CategoryDTO? GetCategory(Guid id)
        {
            var category = _categoryRepository.Find(id);
            return _mapper.Map<CategoryDTO?>(category);
        }

        public List<CategoryDTO> GetCategories(Guid? parentId)
        {
            var categories = _categoryRepository.GetAll(parentId);
            return _mapper.Map<List<CategoryDTO>>(categories);
        }
    }
}
