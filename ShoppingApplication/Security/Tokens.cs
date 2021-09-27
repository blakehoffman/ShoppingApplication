using Application.DTO.Authentication;
using Infrastructure.Records;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Security
{
    public static class Tokens
    {

        public static JwtSecurityToken DeserializeJwtToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.ReadJwtToken(token);
        }

        public static string GenerateToken(ApplicationUser user, string key, DateTime expirationDate, List<Claim> claims = null)
        {
            var authenticateTokensDTO = new AuthenticationTokensDTO();
            var tokenHandler = new JwtSecurityTokenHandler();
            var keyBytes = Encoding.ASCII.GetBytes(key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id)
                }),
                Expires = expirationDate,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            if (claims != null)
            {
                tokenDescriptor.Subject.AddClaims(claims);
            }

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(securityToken);
        }

        public static bool ValidateToken(string token, string key)
        {
            SecurityToken securityToken;
            var tokenHandler = new JwtSecurityTokenHandler();
            var keyBytes = Encoding.ASCII.GetBytes(key);
            var tokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
                ClockSkew = TimeSpan.Zero,
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateLifetime = true
            };

            try
            {
                var result = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}
