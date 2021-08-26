using AutoMapper;
using Domain.Models.Order;
using Infrastructure.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mappings
{
    public class OrderMappingsProfile : Profile
    {
        public OrderMappingsProfile()
        {
            CreateMap<Order, OrderRecord>();
            CreateMap<List<OrderProductRecord>, Order>()
                .ForMember(order => order.Products, opt => opt.MapFrom(orderProductRecords => orderProductRecords));
            CreateMap<OrderRecord?, Order?>();
            CreateMap<Product, OrderProductRecord>();
        }
    }
}
