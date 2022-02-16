using Application.DTO.Administrator;
using AutoMapper;
using Domain.Models.Administrator;

namespace Application.Mapping
{
    public class AdministratorDTOMappingsProfile : Profile
    {
        public AdministratorDTOMappingsProfile()
        {
            CreateMap<Administrator, AdministratorDTO>();
        }
    }
}
