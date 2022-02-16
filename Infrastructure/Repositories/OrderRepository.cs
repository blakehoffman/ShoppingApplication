using Dapper;
using Domain.Models.Order;
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
    public class OrderRepository : IOrderRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(Order order)
        {
            var storedProc = "InsertOrder";
            var orderRecord = OrderMapper.MapToOrderRecord(order);

            _unitOfWork.Connection.Execute(
                storedProc,
                orderRecord,
                _unitOfWork.Transaction,
                commandType: CommandType.StoredProcedure);

            storedProc = "InsertOrderProduct";

            foreach (var orderProduct in order.Products)
            {
                var orderProductRecord = OrderMapper.MapProductToOrderProductRecord(orderProduct, order.Id);

                _unitOfWork.Connection.Execute(
                    storedProc,
                    null,
                    _unitOfWork.Transaction,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public Order Find(Guid id)
        {
            var storedProc = "GetOrder";
            OrderRecord orderRecord;

            orderRecord = _unitOfWork.Connection.Query<OrderRecord>(
                storedProc,
                new { id },
                _unitOfWork.Transaction,
                commandType: CommandType.StoredProcedure)
                .FirstOrDefault();

            if (orderRecord == null)
            {
                return null;
            }

            List<OrderProductRecord> orderProductRecords;
            storedProc = "GetOrderProducts";

            orderProductRecords = _unitOfWork.Connection.Query<OrderProductRecord>(
                storedProc,
                new { id },
                _unitOfWork.Transaction,
                commandType: CommandType.StoredProcedure)
                .ToList();

            storedProc = "GetProduct";
            var products = new List<Product>(); ;

            if (orderProductRecords != null)
            {
                foreach (var orderProductRecord in orderProductRecords)
                {
                    ProductRecord productRecord;

                    productRecord = _unitOfWork.Connection.Query<ProductRecord>(
                        storedProc,
                        new { Id = orderProductRecord.ProductId },
                        _unitOfWork.Transaction,
                        commandType: CommandType.StoredProcedure)
                        .FirstOrDefault();

                    products.Add(OrderMapper.MapOrderProductRecordToProduct(orderProductRecord, productRecord));
                }
            }

            DiscountRecord discountRecord = null;
            storedProc = "GetDiscount";

            discountRecord = _unitOfWork.Connection.Query<DiscountRecord>(
                storedProc,
                new { Id = orderRecord.DiscountId },
                _unitOfWork.Transaction,
                commandType: CommandType.StoredProcedure)
                .FirstOrDefault();

            Discount discount = OrderMapper.MapDiscountRecordToDiscount(discountRecord);
            var order = OrderMapper.MapOrderRecordToOrder(orderRecord, products, discount);

            return order;
        }

        public List<Order> GetAll()
        {
            var storedProc = "GetOrders";
            List<OrderRecord> orderRecords;

            orderRecords = _unitOfWork.Connection.Query<OrderRecord>(
                storedProc,
                transaction: _unitOfWork.Transaction,
                commandType: CommandType.StoredProcedure)
                .ToList();

            storedProc = "GetOrderProduct";
            var orders = new List<Order>();

            foreach (var orderRecord in orderRecords)
            {
                List<OrderProductRecord> orderProductRecords;
                storedProc = "GetOrderProducts";

                orderProductRecords = _unitOfWork.Connection.Query<OrderProductRecord>(
                    storedProc,
                    new { orderRecord.Id },
                    _unitOfWork.Transaction,
                    commandType: CommandType.StoredProcedure)
                    .ToList();

                storedProc = "GetProduct";
                var products = new List<Product>(); ;

                if (orderProductRecords != null)
                {
                    foreach (var orderProductRecord in orderProductRecords)
                    {
                        ProductRecord productRecord;

                        productRecord = _unitOfWork.Connection.Query<ProductRecord>(
                            storedProc,
                            new { Id = orderProductRecord.ProductId },
                            _unitOfWork.Transaction,
                            commandType: CommandType.StoredProcedure)
                            .FirstOrDefault();

                        products.Add(OrderMapper.MapOrderProductRecordToProduct(orderProductRecord, productRecord));
                    }
                }

                DiscountRecord discountRecord = null;
                storedProc = "GetDiscount";

                discountRecord = _unitOfWork.Connection.Query<DiscountRecord>(
                    storedProc,
                    new { Id = orderRecord.DiscountId },
                    _unitOfWork.Transaction,
                    commandType: CommandType.StoredProcedure)
                    .FirstOrDefault();

                Discount discount = OrderMapper.MapDiscountRecordToDiscount(discountRecord);
                var order = OrderMapper.MapOrderRecordToOrder(orderRecord, products, discount);
                orders.Add(order);
            }

            return orders;
        }

        public List<Order> GetUsersOrders(string userID)
        {
            var storedProc = "GetOrdersByUserID";
            List<OrderRecord> orderRecords;

            orderRecords = _unitOfWork.Connection.Query<OrderRecord>(
                storedProc,
                new { userID },
                _unitOfWork.Transaction,
                commandType: CommandType.StoredProcedure)
                .ToList();

            storedProc = "GetOrderProduct";
            var orders = new List<Order>();

            foreach (var orderRecord in orderRecords)
            {
                List<OrderProductRecord> orderProductRecords;
                storedProc = "GetOrderProducts";

                orderProductRecords = _unitOfWork.Connection.Query<OrderProductRecord>(
                    storedProc,
                    new { orderRecord.Id },
                    _unitOfWork.Transaction,
                    commandType: CommandType.StoredProcedure)
                    .ToList();

                storedProc = "GetProduct";
                var products = new List<Product>(); ;

                if (orderProductRecords != null)
                {
                    foreach (var orderProductRecord in orderProductRecords)
                    {
                        ProductRecord productRecord;

                        productRecord = _unitOfWork.Connection.Query<ProductRecord>(
                            storedProc,
                            new { Id = orderProductRecord.ProductId },
                            _unitOfWork.Transaction,
                            commandType: CommandType.StoredProcedure)
                            .FirstOrDefault();

                        products.Add(OrderMapper.MapOrderProductRecordToProduct(orderProductRecord, productRecord));
                    }
                }

                DiscountRecord discountRecord = null;
                storedProc = "GetDiscount";

                discountRecord = _unitOfWork.Connection.Query<DiscountRecord>(
                    storedProc,
                    new { Id = orderRecord.DiscountId },
                    _unitOfWork.Transaction,
                    commandType: CommandType.StoredProcedure)
                    .FirstOrDefault();

                Discount discount = OrderMapper.MapDiscountRecordToDiscount(discountRecord);
                var order = OrderMapper.MapOrderRecordToOrder(orderRecord, products, discount);
                orders.Add(order);
            }

            return orders;
        }
    }
}
