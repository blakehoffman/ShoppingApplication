using AutoMapper;
using Dapper;
using Domain.Models.Discount;
using Domain.Repositories;
using Infrastructure.Records;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly string _connectionString;
        private readonly IMapper _mapper;

        public DiscountRepository(string connectionString, IMapper mapper)
        {
            _connectionString = connectionString;
            _mapper = mapper;
        }

        public void Add(Discount discount)
        {
            var storedProc = "InsertDiscount";
            var discountRecord = _mapper.Map<DiscountRecord>(discount);

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(storedProc, discountRecord);
            }
        }

        public Discount? Find(Guid id)
        {
            var storedProc = "GetDiscount";
            DiscountRecord? discountRecord;

            using ( var connection = new SqlConnection(_connectionString))
            {
                discountRecord = connection.Query<DiscountRecord>(storedProc, id).FirstOrDefault();
            }

            return _mapper.Map<Discount?>(discountRecord);
        }

        public List<Discount> GetAll()
        {
            var storedProc = "GetDiscounts";
            List<DiscountRecord> discountRecords;

            using (var connection = new SqlConnection(_connectionString))
            {
                discountRecords = connection.Query<DiscountRecord>(storedProc).ToList();
            }

            return _mapper.Map<List<Discount>>(discountRecords);
        }
    }
}
