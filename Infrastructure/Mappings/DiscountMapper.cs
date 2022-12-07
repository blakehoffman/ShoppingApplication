using Domain.Models.Discount;
using EntityModels = Infrastructure.Models;

namespace Infrastructure.Mappings
{
    public static class DiscountMapper
    {
        public static Discount MapToDiscount(EntityModels.Discount discount)
        {
            if (discount == null)
            {
                return null;
            }

            return new Discount(discount.Id, discount.Code, discount.Amount);
        }

        public static EntityModels.Discount MapToDiscountEntity(Discount discount)
        {
            if (discount == null)
            {
                return null;
            }

            return new EntityModels.Discount
            {
                Id = discount.Id,
                Code = discount.Code,
                Amount = discount.Amount,
                Active = discount.Active
            };
        }
    }
}
