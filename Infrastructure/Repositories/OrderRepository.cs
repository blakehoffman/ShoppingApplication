using AutoMapper;
using Dapper;
using Domain.Models.Order;
using Domain.Repositories;
using Infrastructure.Records;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly string _connectionString;
        private readonly IMapper _mapper;

        public OrderRepository(string connectionString, IMapper mapper)
        {
            _connectionString = connectionString;
            _mapper = mapper;
        }

        public void Add(Order order)
        {
            var storedProc = "InsertOrder";
            var orderRecord = _mapper.Map<OrderRecord>(order);

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(storedProc, orderRecord);
            }

            storedProc = "InsertOrderProduct";

            foreach (var orderProduct in order.Products)
            {
                var orderProductRecord = _mapper.Map<OrderProductRecord>(orderProduct);

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Execute(storedProc, orderProductRecord);
                }
            }
        }

        public Order? Find(Guid id)
        {
            var storedProc = "GetOrder";
            OrderRecord? orderRecord;

            using (var connection = new SqlConnection(_connectionString))
            {
                orderRecord = connection.Query<OrderRecord>(storedProc, id).FirstOrDefault();
            }

            List<OrderProductRecord> orderProductRecords;
            storedProc = "GetOrderProducts";

            using (var connection = new SqlConnection(_connectionString))
            {
                orderProductRecords = connection.Query<OrderProductRecord>(storedProc, id).ToList();
            }

            var order = _mapper.Map<Order>(orderRecord);
            return _mapper.Map(orderProductRecords, order);
        }

        public List<Order> GetAll()
        {
            var storedProc = "GetOrders";
            List<OrderRecord> orderRecords;

            using (var connection = new SqlConnection(_connectionString))
            {
                orderRecords = connection.Query<OrderRecord>(storedProc).ToList();
            }

            storedProc = "GetOrderProduct";
            var orders = new List<Order>();

            foreach (var orderRecord in orderRecords)
            {
                var order = _mapper.Map<Order>(orderRecord);

                using (var connection = new SqlConnection(_connectionString))
                {
                    var orderProductRecords = connection.Query<OrderProductRecord>(storedProc, order.Id).ToList();
                    orders.Add(_mapper.Map(orderProductRecords, order));
                }
            }

            return orders;
        }
    }
}
