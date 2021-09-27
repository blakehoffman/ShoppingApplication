using Domain.Models.Discount;
using Infrastructure.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mappings
{
    public static class DiscountMapper
    {
        public static Discount? MapToDiscount(DiscountRecord? discountRecord)
        {
            if (discountRecord == null)
            {
                return null;
            }

            return new Discount(discountRecord.Id, discountRecord.Code, discountRecord.Amount);
        }

        public static DiscountRecord? MapToDiscountRecord(Discount? discount)
        {
            if (discount == null)
            {
                return null;
            }

            return new DiscountRecord
            {
                Id = discount.Id,
                Code = discount.Code,
                Amount = discount.Amount,
                Active = discount.Active
            };
        }
    }
}
