using Domain.Models.Order;
using System;
using System.Collections.Generic;
using EntityModels = Infrastructure.Models;

namespace Infrastructure.Mappings
{
    public static class OrderMapper
    {

        public static Discount MapDiscountEntityToDiscount(EntityModels.Discount discountEntity)
        {
            if (discountEntity == null)
            {
                return null;
            }

            return new Discount(discountEntity.Id, discountEntity.Amount);
        }

        public static Product MapOrderProductEntityToProduct(EntityModels.OrderProduct orderProductEntity)
        {
            return new Product(
                orderProductEntity.Product.Id,
                orderProductEntity.Product.Name,
                orderProductEntity.Product.Price,
                orderProductEntity.Quantity);
        }

        public static Order MapOrderEntityToOrder(EntityModels.Order order)
        {
            if (order == null)
            {
                return null;
            }

            var discount = MapDiscountEntityToDiscount(order.Discount);
            var orderProducts = new List<Product>();

            foreach (var orderProduct in order.Products)
            {
                orderProducts.Add(MapOrderProductEntityToProduct(orderProduct));
            }

            return new Order(
                order.Id,
                order.UserId.ToString(),
                order.OrderDate,
                discount,
                orderProducts
            );
        }

        public static EntityModels.OrderProduct MapProductToOrderProductEntity(Product product, Guid orderId)
        {
            if (product == null)
            {
                return null;
            }

            return new EntityModels.OrderProduct
            {
                OrderId = orderId,
                ProductId = product.Id,
                Quantity = product.Quantity,
            };
        }

        public static EntityModels.Order MapToOrderEntity(Order order)
        {
            if (order == null)
            {
                return null;
            }

            return new EntityModels.Order
            {
                Id = order.Id,
                UserId = new Guid(order.UserId),
                OrderDate = order.DateTime,
                DiscountId = order.Discount
            };
        }
    }
}
