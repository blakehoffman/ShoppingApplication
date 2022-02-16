using Application.DTO.Order;
using AutoMapper;
using Domain.Models.Order;

namespace Application.Mapping
{
    public class OrderDTOMappingsProfile : Profile
    {
        public OrderDTOMappingsProfile()
        {
            CreateMap<Order, OrderDTO>()
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.GetTotal()));
            CreateMap<Product, OrderProductDTO>();
        }
    }
}
