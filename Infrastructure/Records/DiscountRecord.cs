using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Records
{
    public class DiscountRecord
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public double Amount { get; set; }
        public bool Active { get; set; }
    }
}
