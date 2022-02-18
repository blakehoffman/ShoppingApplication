using System;

namespace Application.DTO.Cart
{
    public class AddCartProductDTO
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
    }
}
