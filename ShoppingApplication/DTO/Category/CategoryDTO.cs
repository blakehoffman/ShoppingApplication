using System;

namespace Application.DTO.Category
{
    public class CategoryDTO
    {
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
    }
}
