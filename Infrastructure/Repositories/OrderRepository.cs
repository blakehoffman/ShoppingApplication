using AutoMapper;
using Dapper;
using Domain.Models.Order;
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
    public class OrderRepository : IOrderRepository
    {
        private readonly string _connectionString;

        public OrderRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(Order order)
        {
            var storedProc = "InsertOrder";
            var orderRecord = OrderMapper.MapToOrderRecord(order);

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(
                    storedProc,
                    orderRecord,
                    commandType: CommandType.StoredProcedure);
            }

            storedProc = "InsertOrderProduct";

            foreach (var orderProduct in order.Products)
            {
                var orderProductRecord = OrderMapper.MapProductToOrderProductRecord(orderProduct, order.Id);

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Execute(
                        storedProc,
                        orderProductRecord,
                        commandType: CommandType.StoredProcedure);
                }
            }
        }

        public Order? Find(Guid id)
        {
            var storedProc = "GetOrder";
            OrderRecord? orderRecord;

            using (var connection = new SqlConnection(_connectionString))
            {
                orderRecord = connection.Query<OrderRecord>(
                    storedProc,
                    new { id },
                    commandType: CommandType.StoredProcedure)
                    .FirstOrDefault();
            }

            if (orderRecord == null)
            {
                return null;
            }

            List<OrderProductRecord> orderProductRecords;
            storedProc = "GetOrderProducts";

            using (var connection = new SqlConnection(_connectionString))
            {
                orderProductRecords = connection.Query<OrderProductRecord>(
                    storedProc,
                    new { id },
                    commandType: CommandType.StoredProcedure)
                    .ToList();
            }

            storedProc = "GetProduct";
            var products = new List<Product>(); ;

            if (orderProductRecords != null)
            {
                foreach (var orderProductRecord in orderProductRecords)
                {
                    ProductRecord? productRecord;

                    using (var connection = new SqlConnection(_connectionString))
                    {
                        productRecord = connection.Query<ProductRecord?>(storedProc,
                            new { Id = orderProductRecord.ProductId },
                            commandType: CommandType.StoredProcedure)
                            .FirstOrDefault();
                    }

                    products.Add(OrderMapper.MapOrderProductRecordToProduct(orderProductRecord, productRecord));
                }
            }

            DiscountRecord? discountRecord = null;
            storedProc = "GetDiscount";

            using (var connection = new SqlConnection(_connectionString))
            {
                discountRecord = connection.Query<DiscountRecord>(
                    storedProc,
                    new { Id = orderRecord.DiscountId },
                    commandType: CommandType.StoredProcedure)
                    .FirstOrDefault();
            }

            Discount? discount = OrderMapper.MapDiscountRecordToDiscount(discountRecord);
            var order = OrderMapper.MapOrderRecordToOrder(orderRecord, products, discount);

            return order;
        }

        public List<Order> GetAll()
        {
            var storedProc = "GetOrders";
            List<OrderRecord> orderRecords;

            using (var connection = new SqlConnection(_connectionString))
            {
                orderRecords = connection.Query<OrderRecord>(
                    storedProc,
                    commandType: CommandType.StoredProcedure)
                    .ToList();
            }

            storedProc = "GetOrderProduct";
            var orders = new List<Order>();

            foreach (var orderRecord in orderRecords)
            {
                List<OrderProductRecord> orderProductRecords;
                storedProc = "GetOrderProducts";

                using (var connection = new SqlConnection(_connectionString))
                {
                    orderProductRecords = connection.Query<OrderProductRecord>(
                        storedProc,
                        new { orderRecord.Id },
                        commandType: CommandType.StoredProcedure)
                        .ToList();
                }

                storedProc = "GetProduct";
                var products = new List<Product>(); ;

                if (orderProductRecords != null)
                {
                    foreach (var orderProductRecord in orderProductRecords)
                    {
                        ProductRecord? productRecord;

                        using (var connection = new SqlConnection(_connectionString))
                        {
                            productRecord = connection.Query<ProductRecord?>(storedProc,
                                new { Id = orderProductRecord.ProductId },
                                commandType: CommandType.StoredProcedure)
                                .FirstOrDefault();
                        }

                        products.Add(OrderMapper.MapOrderProductRecordToProduct(orderProductRecord, productRecord));
                    }
                }

                DiscountRecord? discountRecord = null;
                storedProc = "GetDiscount";

                using (var connection = new SqlConnection(_connectionString))
                {
                    discountRecord = connection.Query<DiscountRecord>(
                        storedProc,
                        new { Id = orderRecord.DiscountId },
                        commandType: CommandType.StoredProcedure)
                        .FirstOrDefault();
                }

                Discount? discount = OrderMapper.MapDiscountRecordToDiscount(discountRecord);
                var order = OrderMapper.MapOrderRecordToOrder(orderRecord, products, discount);
                orders.Add(order);
            }

            return orders;
        }

        public List<Order> GetUsersOrders(string userID)
        {
            var storedProc = "GetOrdersByUserID";
            List<OrderRecord> orderRecords;

            using (var connection = new SqlConnection(_connectionString))
            {
                orderRecords = connection.Query<OrderRecord>(
                    storedProc,
                    new { userID },
                    commandType: CommandType.StoredProcedure)
                    .ToList();
            }

            storedProc = "GetOrderProduct";
            var orders = new List<Order>();

            foreach (var orderRecord in orderRecords)
            {
                List<OrderProductRecord> orderProductRecords;
                storedProc = "GetOrderProducts";

                using (var connection = new SqlConnection(_connectionString))
                {
                    orderProductRecords = connection.Query<OrderProductRecord>(
                        storedProc,
                        new { orderRecord.Id },
                        commandType: CommandType.StoredProcedure)
                        .ToList();
                }

                storedProc = "GetProduct";
                var products = new List<Product>(); ;

                if (orderProductRecords != null)
                {
                    foreach (var orderProductRecord in orderProductRecords)
                    {
                        ProductRecord? productRecord;

                        using (var connection = new SqlConnection(_connectionString))
                        {
                            productRecord = connection.Query<ProductRecord?>(storedProc,
                                new { Id = orderProductRecord.ProductId },
                                commandType: CommandType.StoredProcedure)
                                .FirstOrDefault();
                        }

                        products.Add(OrderMapper.MapOrderProductRecordToProduct(orderProductRecord, productRecord));
                    }
                }

                DiscountRecord? discountRecord = null;
                storedProc = "GetDiscount";

                using (var connection = new SqlConnection(_connectionString))
                {
                    discountRecord = connection.Query<DiscountRecord>(
                        storedProc,
                        new { Id = orderRecord.DiscountId },
                        commandType: CommandType.StoredProcedure)
                        .FirstOrDefault();
                }

                Discount? discount = OrderMapper.MapDiscountRecordToDiscount(discountRecord);
                var order = OrderMapper.MapOrderRecordToOrder(orderRecord, products, discount);
                orders.Add(order);
            }

            return orders;
        }
    }
}
