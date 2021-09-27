using Domain.Models.Order;
using Infrastructure.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mappings
{
    public static class OrderMapper
    {

        public static Discount? MapDiscountRecordToDiscount(DiscountRecord? discountRecord)
        {
            if (discountRecord == null)
            {
                return null;
            }

            return new Discount(discountRecord.Id, discountRecord.Amount);
        }

        public static Product? MapOrderProductRecordToProduct(OrderProductRecord orderProductRecord, ProductRecord productRecord)
        {
            if (orderProductRecord == null)
            {
                return null;
            }

            return new Product(orderProductRecord.ProductId, productRecord.Name, productRecord.Price, orderProductRecord.Quantity);
        }

        public static Order? MapOrderRecordToOrder(OrderRecord orderRecord, List<Product> orderProducts, Discount? discount)
        {
            if (orderRecord != null && orderProducts == null)
            {
                return new Order(orderRecord.Id, orderRecord.UserId.ToString(), orderRecord.OrderDate, discount);
            }
            else if (orderRecord != null && orderProducts != null)
            {
                return new Order(orderRecord.Id, orderRecord.UserId.ToString(), orderRecord.OrderDate, discount, orderProducts);
            }
            else
            {
                return null;
            }
        }

        public static OrderProductRecord? MapProductToOrderProductRecord(Product? product, Guid orderId)
        {
            if (product == null)
            {
                return null;
            }

            return new OrderProductRecord
            {
                OrderId = orderId,
                ProductId = product.Id,
                Quantity = product.Quantity,
            };
        }

        public static OrderRecord? MapToOrderRecord(Order? order)
        {
            if (order == null)
            {
                return null;
            }

            return new OrderRecord
            {
                Id = order.Id,
                UserId = new Guid(order.UserId),
                OrderDate = order.DateTime,
                DiscountId = order.Discount
            };
        }
    }
}
