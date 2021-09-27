using Application.DTO.Product;
using AutoMapper;
using Domain.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class ProductDTOMappingsProfile : Profile
    {
        public ProductDTOMappingsProfile()
        {
            CreateMap<Product?, ProductDTO?>();
        }
    }
}
