using Domain.Models.Discount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IDiscountRepository
    {
        public void Add(Discount discount);
        public Discount? Find(Guid id);
        public Discount? FindByCode(string code);
        public List<Discount> GetAll();
    }
}
