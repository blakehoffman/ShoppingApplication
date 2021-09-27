using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTO.Cart
{
    public class UpdateProductQuantityInCartDTO
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
