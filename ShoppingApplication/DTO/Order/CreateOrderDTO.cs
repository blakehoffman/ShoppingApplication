using System;
using System.Collections.Generic;

namespace Application.DTO.Order
{
    public class CreateOrderDTO
    {
        public List<CreateOrderProductDTO> Products { get; set; }
        public string Discount { get; set; }
    }
}
