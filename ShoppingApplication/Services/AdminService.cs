using Application.DTO;
using Application.DTO.Administrator;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Models.Administrator;
using Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdministratorRepository _administratorRepository;
        private readonly IMapper _mapper;

        public AdminService(
            IAdministratorRepository administratorRepository,
            IMapper mapper)
        {
            _administratorRepository = administratorRepository;
            _mapper = mapper;
        }

        public async Task<ResultDTO> CreateAdministrator(CreateAdministratorDTO createAdministratorDTO)
        {
            var resultDTO = new ResultDTO();

            if (string.IsNullOrEmpty(createAdministratorDTO.Email) || createAdministratorDTO.Email.Length > 100)
            {
                resultDTO.Errors.Add("Email cannot be empty and cannot be greater than 100 characters");
            }

            if (await _administratorRepository.Find(createAdministratorDTO.Email) != null)
            {
                resultDTO.Errors.Add("Admin already exists for this email");
            }

            if (resultDTO.Errors.Count > 0)
            {
                resultDTO.Succeeded = false;
                return resultDTO;
            }

            var administrator = new Administrator(createAdministratorDTO.Email);
            await _administratorRepository.Add(administrator);
            resultDTO.Succeeded = true;

            return resultDTO;
        }

        public async Task<List<AdministratorDTO>> GetAdministrators()
        {
            var administrators = await _administratorRepository.GetAll();
            return _mapper.Map<List<AdministratorDTO>>(administrators);
        }
    }
}
