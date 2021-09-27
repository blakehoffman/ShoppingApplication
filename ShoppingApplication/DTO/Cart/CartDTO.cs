using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTO.Cart
{
    public class CartDTO
    {
        public Guid Id { get; set; }
        public List<CartProductDTO> Products { get; set; }
    }
}
