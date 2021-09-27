using AutoMapper;
using Dapper;
using Domain.Models.Discount;
using Domain.Repositories;
using Infrastructure.Mappings;
using Infrastructure.Records;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly string _connectionString;

        public DiscountRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(Discount discount)
        {
            var storedProc = "InsertDiscount";
            var discountRecord = DiscountMapper.MapToDiscountRecord(discount);

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(
                    storedProc,
                    discountRecord,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public Discount? Find(Guid id)
        {
            var storedProc = "GetDiscount";
            DiscountRecord? discountRecord;

            using ( var connection = new SqlConnection(_connectionString))
            {
                discountRecord = connection.Query<DiscountRecord>(
                    storedProc,
                    new { id },
                    commandType: CommandType.StoredProcedure)
                    .FirstOrDefault();
            }

            return DiscountMapper.MapToDiscount(discountRecord);
        }

        public Discount? FindByCode(string code)
        {
            var storedProc = "FindDiscountByCode";
            DiscountRecord? discountRecord;

            using (var connection = new SqlConnection(_connectionString))
            {
                discountRecord = connection.Query<DiscountRecord>(
                    storedProc,
                    new { code },
                    commandType: CommandType.StoredProcedure)
                    .FirstOrDefault();
            }

            return DiscountMapper.MapToDiscount(discountRecord);
        }

        public List<Discount> GetAll()
        {
            var storedProc = "GetDiscounts";
            List<DiscountRecord> discountRecords;

            using (var connection = new SqlConnection(_connectionString))
            {
                discountRecords = connection.Query<DiscountRecord>(
                    storedProc,
                    commandType: CommandType.StoredProcedure)
                    .ToList();
            }

            var discounts = new List<Discount>();

            foreach (var discount in discountRecords)
            {
                discounts.Add(DiscountMapper.MapToDiscount(discount));
            }

            return discounts;
        }
    }
}
