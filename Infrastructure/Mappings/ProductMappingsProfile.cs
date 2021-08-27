using AutoMapper;
using Domain.Models.Product;
using Infrastructure.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mappings
{
    public class ProductMappingsProfile : Profile
    {
        public ProductMappingsProfile()
        {
            CreateMap<Product, ProductRecord>();
            CreateMap<ProductRecord?, Product?>();
        }
    }
}
