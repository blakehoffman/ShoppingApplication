using System;

namespace Application.DTO.Cart
{
    public class CartProductDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
    }
}
