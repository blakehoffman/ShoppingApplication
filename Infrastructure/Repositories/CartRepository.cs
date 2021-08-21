using AutoMapper;
using Dapper;
using Domain.Models.Cart;
using Domain.Repositories;
using Infrastructure.Records;
using System.Data.SqlClient;

namespace Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly string _connectionString;
        private readonly IMapper _mapper;

        public CartRepository(string connectionString, IMapper mapper)
        {
            _connectionString = connectionString;
            _mapper = mapper;
        }

        public void Add(Cart cart)
        {
            var storedProc = "InsertCart";
            var cartRecord = _mapper.Map<CartRecord>(cart);

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(storedProc, cartRecord);
            }

            storedProc = "InsertCartProduct";

            foreach (var cartProduct in cart.Products)
            {
                var cartProductRecord = _mapper.Map<CartProductRecord>(cartProduct);

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Execute(storedProc, cartProductRecord);
                }
            }
        }

        public Cart? Find(Guid cartId)
        {
            var storedProc = "GetCart";
            CartRecord? cartRecord;

            using (var connection = new SqlConnection(_connectionString))
            {
                cartRecord = connection.Query<CartRecord>(storedProc, cartId).FirstOrDefault();
            }

            List<CartProductRecord> cartProductRecords;
            storedProc = "GetCartProducts";

            using (var connection = new SqlConnection(_connectionString))
            {
                cartProductRecords = connection.Query<CartProductRecord>(storedProc, cartId).ToList();
            }

            var cart = _mapper.Map<Cart>(cartRecord);
            return _mapper.Map(cartProductRecords, cart);
        }
    }
}
