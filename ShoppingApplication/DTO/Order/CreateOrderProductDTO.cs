using System;

namespace Application.DTO.Order
{
    public class CreateOrderProductDTO
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
    }
}
