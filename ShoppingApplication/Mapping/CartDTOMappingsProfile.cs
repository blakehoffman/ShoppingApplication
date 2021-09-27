using Application.DTO.Cart;
using AutoMapper;
using Domain.Models.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class CartDTOMappingsProfile : Profile
    {
        public CartDTOMappingsProfile()
        {
            CreateMap<Cart?, CartDTO>();
            CreateMap<Product, CartProductDTO>();
        }
    }
}
