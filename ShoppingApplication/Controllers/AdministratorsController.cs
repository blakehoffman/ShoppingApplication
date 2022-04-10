using Application.DTO;
using Application.DTO.Administrator;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
        public ActionResult<List<AdministratorDTO>> GetAllAdministrators()
        {
            return _adminService.GetAdministrators();
        }


        [HttpPost]
        public ActionResult<ResultDTO> Create(CreateAdministratorDTO createAdministratorDTO)
        {
            var result = _adminService.CreateAdministrator(createAdministratorDTO);
            return result;
        }
    }
}
