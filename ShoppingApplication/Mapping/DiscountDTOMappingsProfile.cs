using Application.DTO.Discount;
using AutoMapper;
using Domain.Models.Discount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class DiscountDTOMappingsProfile : Profile
    {
        public DiscountDTOMappingsProfile()
        {
            CreateMap<Discount?, DiscountDTO?>();
        }
    }
}
