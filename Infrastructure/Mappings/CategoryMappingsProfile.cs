using AutoMapper;
using Domain.Models.Categories;
using Infrastructure.Records;

namespace Infrastructure.Mappings
{
    class CategoryMappingsProfile : Profile
    {
        public CategoryMappingsProfile()
        {
            CreateMap<Category, CategoryRecord>();
            CreateMap<CategoryRecord?, Category>();
            CreateMap<List<CategoryRecord>, List<Category>>();
        }
    }
}
