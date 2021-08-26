using Domain.Models.Cart;
using Infrastructure.Records;
using AutoMapper;
using System.Collections.Generic;

namespace Infrastructure.Mappings
{
    public class CartMappingsProfile : Profile
    {
        public CartMappingsProfile()
        {
            CreateMap<Cart, CartRecord>();
            CreateMap<List<CartProductRecord>, Cart>()
                .ForMember(cart => cart.Products, opt => opt.MapFrom(cartProductRecords => cartProductRecords));
            CreateMap<CartRecord?, Cart?>();
            CreateMap<Product, CartProductRecord>();
        }
    }
}
