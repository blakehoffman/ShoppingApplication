using Domain.Models.Discount;
using System;
using System.Collections.Generic;

namespace Domain.Repositories
{
    public interface IDiscountRepository
    {
        public void Add(Discount discount);
        public Discount Find(Guid id);
        public Discount FindByCode(string code);
        public List<Discount> GetAll();
    }
}
