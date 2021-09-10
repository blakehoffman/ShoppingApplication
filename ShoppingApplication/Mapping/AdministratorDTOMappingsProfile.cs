using Application.DTO.Administrator;
using AutoMapper;
using Domain.Models.Administrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class AdministratorDTOMappingsProfile : Profile
    {
        public AdministratorDTOMappingsProfile()
        {
            CreateMap<Administrator?, AdministratorDTO?>();
        }
    }
}
