using Application.DTO.Cart;
using AutoMapper;
using Domain.Models.Cart;

namespace Application.Mapping
{
    public class CartDTOMappingsProfile : Profile
    {
        public CartDTOMappingsProfile()
        {
            CreateMap<Cart, CartDTO>();
            CreateMap<Product, CartProductDTO>();
        }
    }
}
