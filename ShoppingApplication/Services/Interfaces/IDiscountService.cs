using Application.DTO;
using Application.DTO.Discount;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IDiscountService
    {
        public Task<ResultDTO> CreateDiscount(CreateDiscountDTO createDiscountDTO);
        public Task<DiscountDTO> GetDiscount(Guid id);
        public Task<List<DiscountDTO>> GetDiscounts();
    }
}
