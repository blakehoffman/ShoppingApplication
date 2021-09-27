using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Configuration
{
    public class AppSettings
    {
        public string JwtSecret { get; set; }
        public string JwtValidAudience { get; set; }
        public string JwtValidIssuer {  get; set; }
    }
}
