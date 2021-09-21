using Dapper;
using Domain.Models.Cart;
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
    public class CartRepository : ICartRepository
    {
        private readonly string _connectionString;

        public CartRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(Cart cart)
        {
            var storedProc = "InsertCart";
            var cartRecord = CartMapper.MapToCartRecord(cart);

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(
                    storedProc,
                    cartRecord,
                    commandType: CommandType.StoredProcedure);
            }

            storedProc = "InsertCartProduct";

            foreach (var cartProduct in cart.Products)
            {
                var cartProductRecord = CartMapper.MapToCartProductRecord(cart.Id, cartProduct);

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Execute(
                        storedProc,
                        cartProductRecord,
                        commandType: CommandType.StoredProcedure);
                }
            }
        }

        public Cart? Find(Guid id)
        {
            var storedProc = "GetCart";
            CartRecord? cartRecord;

            using (var connection = new SqlConnection(_connectionString))
            {
                cartRecord = connection.Query<CartRecord>(
                    storedProc,
                    new { id },
                    commandType: CommandType.StoredProcedure)
                    .FirstOrDefault();
            }

            if (cartRecord == null)
            {
                return null;
            }

            List<CartProductRecord> cartProductRecords;
            storedProc = "GetCartProducts";

            using (var connection = new SqlConnection(_connectionString))
            {
                cartProductRecords = connection.Query<CartProductRecord>(storedProc,
                    new { id },
                    commandType: CommandType.StoredProcedure)
                    .ToList();
            }

            List<Product> products = new List<Product>();
            storedProc = "GetProduct";

            foreach (var cartProductRecord in cartProductRecords)
            {
                ProductRecord? productRecord;

                using (var connection = new SqlConnection(_connectionString))
                {
                    productRecord = connection.Query<ProductRecord?>(storedProc,
                        new { cartProductRecord.ProductId },
                        commandType: CommandType.StoredProcedure)
                        .FirstOrDefault();
                }

                var product = CartMapper.MapToCartProduct(cartProductRecord, productRecord);
                
                if (product != null)
                {
                    products.Add(product);
                }
            }

            return CartMapper.MapToCart(cartRecord, products);
        }

        public Cart? FindByUserId(string userId)
        {
            var storedProc = "GetCartByUserId";
            CartRecord? cartRecord;

            using (var connection = new SqlConnection(_connectionString))
            {
                cartRecord = connection.Query<CartRecord>(storedProc,
                    new { userId },
                    commandType: CommandType.StoredProcedure)
                    .FirstOrDefault();
            }

            if (cartRecord == null)
            {
                return null;
            }

            List<CartProductRecord> cartProductRecords;
            storedProc = "GetCartProducts";

            using (var connection = new SqlConnection(_connectionString))
            {
                cartProductRecords = connection.Query<CartProductRecord>(storedProc,
                    new { cartRecord.Id },
                    commandType: CommandType.StoredProcedure)
                    .ToList();
            }

            List<Product> products = new List<Product>();
            storedProc = "GetProduct";

            foreach (var cartProductRecord in cartProductRecords)
            {
                ProductRecord? productRecord;

                using (var connection = new SqlConnection(_connectionString))
                {
                    productRecord = connection.Query<ProductRecord?>(storedProc,
                        new { Id = cartProductRecord.ProductId },
                        commandType: CommandType.StoredProcedure)
                        .FirstOrDefault();
                }

                var product = CartMapper.MapToCartProduct(cartProductRecord, productRecord);
                
                if (product != null)
                {
                    products.Add(product);
                }
            }

            return CartMapper.MapToCart(cartRecord, products);
        }

        public void Update(Cart cart)
        {
            var storedProc = "DeleteCartProducts";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(storedProc,
                    new { cart.Id },
                    commandType: CommandType.StoredProcedure);
            }

            storedProc = "InsertCartProduct";

            foreach (var cartProduct in cart.Products)
            {
                var cartProductRecord = CartMapper.MapToCartProductRecord(cart.Id, cartProduct);

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Execute(storedProc,
                        cartProductRecord,
                        commandType: CommandType.StoredProcedure);
                }
            }
        }
    }
}
