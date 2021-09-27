using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTO.Authentication
{
    public class AuthenticationTokensDTO
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
