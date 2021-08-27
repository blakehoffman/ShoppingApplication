using AutoMapper;
using Domain.Models.Categories;
using Infrastructure.Records;
using System.Collections.Generic;

namespace Infrastructure.Mappings
{
    class CategoryMappingsProfile : Profile
    {
        public CategoryMappingsProfile()
        {
            CreateMap<Category, CategoryRecord>();
            CreateMap<CategoryRecord?, Category?>();
        }
    }
}
