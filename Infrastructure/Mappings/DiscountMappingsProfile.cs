using AutoMapper;
using Domain.Models.Discount;
using Infrastructure.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mappings
{
    public class DiscountMappingsProfile : Profile
    {
        public DiscountMappingsProfile()
        {
            CreateMap<DiscountRecord?, Discount?>();
            CreateMap<Discount, DiscountRecord>();
            CreateMap<List<DiscountRecord>, List<Discount>>();
        }
    }
}
