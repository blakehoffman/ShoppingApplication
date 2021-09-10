using Application.DTO;
using Application.DTO.Administrator;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("administrators")]
        public ActionResult<List<AdministratorDTO>> GetAllAdministrators()
        {
            return _adminService.GetAdministrators();
        }


        [HttpPost("create-administrator")]
        public ActionResult<ResultDTO> Create(CreateAdministratorDTO createAdministratorDTO)
        {
            var result = _adminService.CreateAdministrator(createAdministratorDTO);
            return result;
        }
    }
}
