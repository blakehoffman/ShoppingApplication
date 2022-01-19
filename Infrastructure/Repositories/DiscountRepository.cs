using Dapper;
using Domain.Models.Discount;
using Domain.Repositories;
using Domain.UnitOfWork;
using Infrastructure.Mappings;
using Infrastructure.Records;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public DiscountRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(Discount discount)
        {
            var storedProc = "InsertDiscount";
            var discountRecord = DiscountMapper.MapToDiscountRecord(discount);

            _unitOfWork.Connection.Execute(
                storedProc,
                discountRecord,
                _unitOfWork.Transaction,
                commandType: CommandType.StoredProcedure);
        }

        public Discount? Find(Guid id)
        {
            var storedProc = "GetDiscount";
            DiscountRecord? discountRecord;

            discountRecord = _unitOfWork.Connection.Query<DiscountRecord>(
                storedProc,
                new { id },
                _unitOfWork.Transaction,
                commandType: CommandType.StoredProcedure)
                .FirstOrDefault();

            return DiscountMapper.MapToDiscount(discountRecord);
        }

        public Discount? FindByCode(string code)
        {
            var storedProc = "FindDiscountByCode";
            DiscountRecord? discountRecord;

            discountRecord = _unitOfWork.Connection.Query<DiscountRecord>(
                storedProc,
                new { code },
                _unitOfWork.Transaction,
                commandType: CommandType.StoredProcedure)
                .FirstOrDefault();

            return DiscountMapper.MapToDiscount(discountRecord);
        }

        public List<Discount> GetAll()
        {
            var storedProc = "GetDiscounts";
            List<DiscountRecord> discountRecords;

            discountRecords = _unitOfWork.Connection.Query<DiscountRecord>(
                storedProc,
                transaction: _unitOfWork.Transaction,
                commandType: CommandType.StoredProcedure)
                .ToList();

            var discounts = new List<Discount>();

            foreach (var discount in discountRecords)
            {
                discounts.Add(DiscountMapper.MapToDiscount(discount));
            }

            return discounts;
        }
    }
}
