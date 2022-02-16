using Application.DTO;
using Application.DTO.Discount;
using System;
using System.Collections.Generic;

namespace Application.Services.Interfaces
{
    public interface IDiscountService
    {
        public ResultDTO CreateDiscount(CreateDiscountDTO createDiscountDTO);
        public DiscountDTO GetDiscount(Guid id);
        public List<DiscountDTO> GetDiscounts();
    }
}
