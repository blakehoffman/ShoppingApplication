using Domain.Models.Cart;
using Infrastructure.Records;
using System;
using System.Collections.Generic;
using EntityModels = Infrastructure.Models;

namespace Infrastructure.Mappings
{
    public static class CartMapper
    {
        public static Cart MapToCart(EntityModels.Cart cartEntity)
        {
            if (cartEntity != null && cartEntity.Products == null)
            {
                return new Cart(cartEntity.Id, cartEntity.UserId.ToString(), cartEntity.DateCreated);
            }
            else if (cartEntity != null && cartEntity != null)
            {
                var cartProducts = new List<Product>();

                foreach (var productEntity in cartEntity.Products)
                {
                    var cartProduct = MapToCartProduct(productEntity);
                    cartProducts.Add(cartProduct);
                }
                return new Cart(cartEntity.Id, cartEntity.UserId.ToString(), cartEntity.DateCreated, cartProducts);
            }
            else
            {
                return null;
            }
        }

        public static EntityModels.Cart MapToCartEntity(Cart cart)
        {
            if (cart == null)
            {
                return null;
            }

            return new EntityModels.Cart
            {
                Id = cart.Id,
                UserId = new Guid(cart.UserId),
                DateCreated = cart.DateCreated,
                Purchased = cart.Purchased
            };
        }

        public static Product MapToCartProduct(EntityModels.CartProduct cartProductEntity)
        {
            if (cartProductEntity == null)
            {
                return null;
            }

            var product = new Product(cartProductEntity.Product.Id, cartProductEntity.Product.Name, cartProductEntity.Product.Price, cartProductEntity.Quantity);
            return product;
        }

        public static EntityModels.CartProduct MapToCartProductEntity(Guid cartId, Product product)
        {
            if (product == null)
            {
                return null;
            }

            return new EntityModels.CartProduct
            {
                CartId = cartId,
                ProductId = product.Id,
                Quantity = product.Quantity
            };
        }
    }
}
