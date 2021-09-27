using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTO.Category
{
    public class CategoryDTO
    {
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
    }
}
