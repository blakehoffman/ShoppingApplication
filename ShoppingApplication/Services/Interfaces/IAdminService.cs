using Application.DTO;
using Application.DTO.Administrator;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IAdminService
    {
        public Task<ResultDTO> CreateAdministrator(CreateAdministratorDTO createAdministratorDTO);
        public Task<List<AdministratorDTO>> GetAdministrators();
    }
}
