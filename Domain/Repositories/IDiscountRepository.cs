using Domain.Models.Discount;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IDiscountRepository
    {
        public Task Add(Discount discount);
        public Task<Discount> Find(Guid id);
        public Task<Discount> FindByCode(string code);
        public Task<List<Discount>> GetAll();
    }
}
