using Domain.Models.Discount;
using Domain.Repositories;
using Domain.UnitOfWork;
using Infrastructure.Contexts;
using Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IUnitOfWork _unitOfWork;

        public DiscountRepository(ApplicationDbContext applicationDbContext, IUnitOfWork unitOfWork)
        {
            _applicationDbContext = applicationDbContext;
            _unitOfWork = unitOfWork;
        }

        public async Task Add(Discount discount)
        {
            var discountEntity = DiscountMapper.MapToDiscountEntity(discount);

            _applicationDbContext.Discounts.Add(discountEntity);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<Discount> Find(Guid id)
        {
            var discount = await _applicationDbContext.Discounts
                .AsNoTracking()
                .FirstOrDefaultAsync(discount => discount.Id == id);

            return DiscountMapper.MapToDiscount(discount);
        }

        public async Task<Discount> FindByCode(string code)
        {
            var discount = await _applicationDbContext.Discounts
                .AsNoTracking()
                .FirstOrDefaultAsync(discount => discount.Code == code);

            return DiscountMapper.MapToDiscount(discount);
        }

        public async Task<List<Discount>> GetAll()
        {
            var discounts = await _applicationDbContext.Discounts
                .AsNoTracking()
                .ToListAsync();

            var domainDiscounts = new List<Discount>();

            foreach (var discount in discounts)
            {
                domainDiscounts.Add(DiscountMapper.MapToDiscount(discount));
            }

            return domainDiscounts;
        }
    }
}
