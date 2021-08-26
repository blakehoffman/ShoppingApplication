using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTO.Category
{
    public class CreateCategoryDTO
    {
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
    }
}
