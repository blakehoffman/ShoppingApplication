using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTO.Discount
{
    public class CreateDiscountDTO
    {
        public string Code { get; set; }
        public double Amount { get; set; }
        public bool Active { get; set; }
    }
}
