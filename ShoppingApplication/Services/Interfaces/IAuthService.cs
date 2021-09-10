using Application.DTO;
using Application.DTO.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<AuthenticationTokensDTO?> Login(LoginDTO loginDTO);
        public Task<AuthenticationTokensDTO?> RefreshTokens(string refreshToken);
        public Task<ResultDTO> Register(RegisterDTO registerDTO);
    }
}
