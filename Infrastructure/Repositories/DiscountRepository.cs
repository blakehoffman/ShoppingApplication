using AutoMapper;
using Dapper;
using Domain.Models.Discount;
using Domain.Repositories;
using Infrastructure.Records;
using System.Data.SqlClient;

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
            DiscountRecord? result;

            using ( var connection = new SqlConnection(_connectionString))
            {
                result = connection.Query<DiscountRecord>(storedProc, id).FirstOrDefault();
            }

            return _mapper.Map<Discount?>(result);
        }

        public List<Discount> GetAll()
        {
            var storedProc = "GetDiscounts";
            List<DiscountRecord> result;

            using (var connection = new SqlConnection(_connectionString))
            {
                result = connection.Query<DiscountRecord>(storedProc).ToList();
            }

            return _mapper.Map<List<Discount>>(result);
        }
    }
}
