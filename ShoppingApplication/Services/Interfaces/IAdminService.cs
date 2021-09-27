using Application.DTO;
using Application.DTO.Administrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IAdminService
    {
        public ResultDTO CreateAdministrator(CreateAdministratorDTO createAdministratorDTO);
        public List<AdministratorDTO> GetAdministrators();
    }
}
