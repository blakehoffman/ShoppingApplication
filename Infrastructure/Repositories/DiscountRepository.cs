using Domain.Models.Discount;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        public void Add(Discount discount)
        {

        }

        public Discount Find(Guid id)
        {
            return null;
        }

        public List<Discount> GetAll()
        {
            return new List<Discount>();
        }
    }
}
