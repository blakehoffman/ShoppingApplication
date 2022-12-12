using System;

namespace Application.DTO.Product
{
    public class CreateProductDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public decimal Price { get; set; }
    }
}
