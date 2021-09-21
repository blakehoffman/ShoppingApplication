using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTO.Cart
{
    public class CartProductDTO
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
    }
}
