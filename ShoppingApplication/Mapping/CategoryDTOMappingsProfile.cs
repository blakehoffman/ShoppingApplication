using Application.DTO.Category;
using AutoMapper;
using Domain.Models.Categories;

namespace Application.Mapping
{
    public class CategoryDTOMappingsProfile : Profile
    {
        public CategoryDTOMappingsProfile()
        {
            CreateMap<Category, CategoryDTO>();
        }
    }
}
