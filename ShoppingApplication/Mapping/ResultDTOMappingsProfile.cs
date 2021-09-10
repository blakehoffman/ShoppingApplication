using Application.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Mapping
{
    public class ResultDTOMappingsProfile : Profile
    {
        public ResultDTOMappingsProfile()
        {
            CreateMap<IdentityResult?, ResultDTO?>()
                .ForMember(resultDTO => resultDTO.Errors,
                    opt => opt.MapFrom(identityResult => identityResult.Errors.Select(e => e.Description).ToList()));
        }
    }
}
