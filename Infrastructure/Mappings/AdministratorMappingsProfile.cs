using AutoMapper;
using Domain.Models.Administrator;
using Infrastructure.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mappings
{
    public class AdministratorMappingsProfile : Profile
    {
        public AdministratorMappingsProfile()
        {
            CreateMap<Administrator, AdministratorRecord>();
            CreateMap<AdministratorRecord?, Administrator?>();
        }
    }
}
