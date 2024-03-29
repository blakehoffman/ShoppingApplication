﻿using Application.DTO;
using Application.DTO.Discount;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Models.Discount;
using Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;
        public DiscountService(IDiscountRepository discountRepository, IMapper mapper)
        {
            _discountRepository = discountRepository;
            _mapper = mapper;
        }

        public async Task<ResultDTO> CreateDiscount(CreateDiscountDTO createDiscountDTO)
        {
            var resultDTO = new ResultDTO();

            if (string.IsNullOrEmpty(createDiscountDTO.Code)
                || createDiscountDTO.Code.Length < 4
                || createDiscountDTO.Code.Length > 50)
            {
                resultDTO.Errors.Add("Discount code cannot be empty and must be between 4 and 50 characters");
            }

            if (createDiscountDTO.Amount <= 0)
            {
                resultDTO.Errors.Add("Discount amount cannot be less than or equal to 0");
            }

            if (await _discountRepository.FindByCode(createDiscountDTO.Code) != null)
            {
                resultDTO.Errors.Add("A discount with this code already exists");
            }

            if (resultDTO.Errors.Count > 0)
            {
                resultDTO.Succeeded = false;
                return resultDTO;
            }

            var discount = new Discount(Guid.NewGuid(), createDiscountDTO.Code, createDiscountDTO.Amount);

            if (createDiscountDTO.Active)
            {
                discount.Active = true;
            }

            await _discountRepository.Add(discount);
            resultDTO.Succeeded = true;

            return resultDTO;
        }

        public async Task<DiscountDTO> GetDiscount(Guid id)
        {
            var discount = await _discountRepository.Find(id);
            return _mapper.Map<DiscountDTO>(discount);
        }

        public async Task<List<DiscountDTO>> GetDiscounts()
        {
            var discounts = await _discountRepository.GetAll();
            return _mapper.Map<List<DiscountDTO>>(discounts);
        }
    }
}
