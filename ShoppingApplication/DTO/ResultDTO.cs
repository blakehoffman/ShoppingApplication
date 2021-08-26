using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class ResultDTO
    {
        public bool IsSuccess { get; set; }
        public List<string> Errors = new List<string>();
    }
}
