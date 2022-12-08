using Application.DTO;
using Application.DTO.Administrator;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "Admin")]
    public class AdministratorsController : Controller
    {
        private readonly IAdminService _adminService;

        public AdministratorsController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        public async Task<ActionResult<List<AdministratorDTO>>> GetAllAdministrators()
        {
            return await _adminService.GetAdministrators();
        }


        [HttpPost]
        public async Task<ActionResult<ResultDTO>> Create(CreateAdministratorDTO createAdministratorDTO)
        {
            var result = await _adminService.CreateAdministrator(createAdministratorDTO);
            return result;
        }
    }
}
