using Application.Configuration;
using Application.DTO;
using Application.DTO.Authentication;
using Application.Security;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Repositories;
using Infrastructure.Records;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAdministratorRepository _administratorRepository;
        private readonly AppSettings _appSettings;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthService(
            IAdministratorRepository administratorRepository,
            AppSettings appSettings,
            IMapper mapper,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager)
        {
            _administratorRepository = administratorRepository;
            _appSettings = appSettings;
            _mapper = mapper;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<AuthenticationTokensDTO?> Login(LoginDTO loginDTO)
        {
            var user = _userManager.Users.FirstOrDefault(user => user.UserName == loginDTO.Username);

            if (user == null)
            {
                return null;
            }

            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, loginDTO.Password);

            if (!isPasswordCorrect)
            {
                return null;
            }

            var roles = await _userManager.GetRolesAsync(user);
            var tokenClaims = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();
            var accessToken = Tokens.GenerateToken(user, _appSettings.JwtSecret, DateTime.UtcNow.AddHours(1), tokenClaims);
            var refreshToken = Tokens.GenerateToken(user, _appSettings.JwtSecret, DateTime.UtcNow.AddDays(1));

            var claims = await _userManager.GetClaimsAsync(user);
            var claim = claims.FirstOrDefault(claim => claim.Type == "RefreshToken");

            if (claim != null)
            {
                await _userManager.RemoveClaimAsync(user, claim);
            }

            await _userManager.AddClaimAsync(user, new Claim("RefreshToken", refreshToken));

            return new AuthenticationTokensDTO
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };
        }

        public async Task<AuthenticationTokensDTO?> RefreshTokens(string refreshToken)
        {
            var isValidRefreshToken = Tokens.ValidateToken(refreshToken, _appSettings.JwtSecret);

            if (!isValidRefreshToken)
            {
                return null;
            }

            var deserializedToken = Tokens.DeserializeJwtToken(refreshToken);
            var usernameClaim = deserializedToken.Claims.Where(c => c.Type == "nameid").FirstOrDefault();

            if (usernameClaim == null)
            {
                return null;
            }

            var user = await _userManager.FindByIdAsync(usernameClaim.Value);

            if (user == null)
            {
                return null;
            }

            var userClaims = await _userManager.GetClaimsAsync(user);
            var refreshTokenClaim = userClaims.Where(c => c.Type == "RefreshToken").FirstOrDefault();

            if (refreshTokenClaim != null && refreshTokenClaim.Value == refreshToken)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var tokenClaims = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();
                var accessToken = Tokens.GenerateToken(user, _appSettings.JwtSecret, DateTime.UtcNow.AddHours(1), tokenClaims);
                var newRefreshToken = Tokens.GenerateToken(user, _appSettings.JwtSecret, DateTime.UtcNow.AddDays(1));

                if (refreshTokenClaim != null)
                {
                    await _userManager.RemoveClaimAsync(user, refreshTokenClaim);
                }

                await _userManager.AddClaimAsync(user, new Claim("RefreshToken", newRefreshToken));

                return new AuthenticationTokensDTO
                {
                    AccessToken = accessToken,
                    RefreshToken = newRefreshToken
                };
            }

            return null;
        }

        public async Task<ResultDTO> Register(RegisterDTO registerDTO)
        {
            var resultDTO = new ResultDTO();

            if (string.IsNullOrEmpty(registerDTO.Firstname) || registerDTO.Firstname.Length > 100)
            {
                resultDTO.Errors.Add("Firstname cannot be empty and cannot be more than 100 characters");
            }

            if (string.IsNullOrEmpty(registerDTO.Lastname) || registerDTO.Lastname.Length > 100)
            {
                resultDTO.Errors.Add("Lastname cannot be empty and cannot be more than 100 characters");
            }

            if (resultDTO.Errors.Count > 0)
            {
                resultDTO.Succeeded = false;
                return resultDTO;
            }

            var user = new ApplicationUser
            {
                Email = registerDTO.Email,
                UserName = registerDTO.Username
            };

            var registrationResult = await _userManager.CreateAsync(user, registerDTO.Password);

            if (!registrationResult.Succeeded)
            {
                return _mapper.Map<ResultDTO>(registrationResult);
            }

            var userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, registerDTO.Firstname),
                new Claim(ClaimTypes.Surname, registerDTO.Lastname),
            };

            var addUserClaimsResult = await _userManager.AddClaimsAsync(user, userClaims);
            
            if (!addUserClaimsResult.Succeeded)
            {
                return _mapper.Map<ResultDTO>(addUserClaimsResult);
            }

            string role = "Customer";

            if (_administratorRepository.Find(registerDTO.Email) != null)
            {
                role = "Admin";
            }

            var addUserToRoleResult = await _userManager.AddToRoleAsync(user, role);

            if (!addUserToRoleResult.Succeeded)
            {
                return _mapper.Map<ResultDTO>(addUserToRoleResult);
            }

            resultDTO.Succeeded = true;
            return resultDTO;
        }
    }
}
