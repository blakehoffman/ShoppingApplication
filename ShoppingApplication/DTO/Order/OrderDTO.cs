using System;
using System.Collections.Generic;

namespace Application.DTO.Order
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
        public List<OrderProductDTO> Products { get; set; }
        public double Total { get; set; }
    }
}
