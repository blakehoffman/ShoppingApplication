using Application.DTO.Product;
using AutoMapper;
using Domain.Models.Product;

namespace Application.Mapping
{
    public class ProductDTOMappingsProfile : Profile
    {
        public ProductDTOMappingsProfile()
        {
            CreateMap<Product, ProductDTO>();
        }
    }
}
