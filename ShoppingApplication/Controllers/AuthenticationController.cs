using Application.DTO;
using Application.DTO.Authentication;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly IAuthService _authenticationService;

        public AuthenticationController(IAuthService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationTokensDTO>> Login(LoginDTO loginDTO)
        {
            var result = await _authenticationService.Login(loginDTO);

            if (result == null)
            {
                return Unauthorized();
            }

            return result;
        }

        [HttpPost("refresh")]
        public async Task<ActionResult<AuthenticationTokensDTO>> Refresh([FromBody] string refreshToken)
        {
            var result = await _authenticationService.RefreshTokens(refreshToken);

            if (result == null)
            {
                return Unauthorized();
            }

            return result;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ResultDTO>> Register([FromBody] RegisterDTO registerDTO)
        {
            var result = await _authenticationService.Register(registerDTO);
            return result;
        }
    }
}
