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
            CreateMap<Discount, DiscountRecord>();
            CreateMap<DiscountRecord?, Discount?>();
            CreateMap<List<DiscountRecord>, List<Discount>>();
        }
    }
}
