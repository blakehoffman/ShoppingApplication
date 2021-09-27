using Domain.Models.Cart;
using Infrastructure.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Mappings
{
    public static class CartMapper
    {
        public static Cart? MapToCart(CartRecord cartRecord, List<Product> cartProducts)
        {
            if (cartRecord != null && cartProducts == null)
            {
                return new Cart(cartRecord.Id, cartRecord.UserId.ToString(), cartRecord.DateCreated);
            }
            else if (cartRecord != null && cartProducts != null)
            {
                return new Cart(cartRecord.Id, cartRecord.UserId.ToString(), cartRecord.DateCreated, cartProducts);
            }
            else
            {
                return null;
            }
        }

        public static CartRecord? MapToCartRecord(Cart cart)
        {
            if (cart == null)
            {
                return null;
            }

            return new CartRecord
            {
                Id = cart.Id,
                UserId = new Guid(cart.UserId),
                DateCreated = cart.DateCreated,
                Purchased = cart.Purchased
            };
        }

        public static Product? MapToCartProduct(CartProductRecord? cartProductRecord, ProductRecord? productRecord)
        {
            if (cartProductRecord == null || productRecord == null)
            {
                return null;
            }

            var product = new Product(productRecord.Id, productRecord.Name, productRecord.Price, cartProductRecord.Quantity);
            return product;
        }

        public static CartProductRecord? MapToCartProductRecord(Guid cartId, Product product)
        {
            if (product == null)
            {
                return null;
            }

            return new CartProductRecord
            {
                CartId = cartId,
                ProductId = product.Id,
                Quantity = product.Quantity
            };
        }
    }
}
