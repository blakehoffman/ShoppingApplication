using Application.DTO.Category;
using AutoMapper;
using Domain.Models.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
