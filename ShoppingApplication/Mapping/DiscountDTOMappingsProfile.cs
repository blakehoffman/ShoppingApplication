using Application.DTO.Discount;
using AutoMapper;
using Domain.Models.Discount;

namespace Application.Mapping
{
    public class DiscountDTOMappingsProfile : Profile
    {
        public DiscountDTOMappingsProfile()
        {
            CreateMap<Discount, DiscountDTO>();
        }
    }
}
