using AutoMapper;
using Domain.Models.Discount;
using Infrastructure.Records;
using System.Collections.Generic;

namespace Infrastructure.Mappings
{
    public class DiscountMappingsProfile : Profile
    {
        public DiscountMappingsProfile()
        {
            CreateMap<Discount, DiscountRecord>();
            CreateMap<DiscountRecord?, Discount?>();
        }
    }
}
