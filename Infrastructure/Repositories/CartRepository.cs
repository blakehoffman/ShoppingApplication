using Dapper;
using Domain.Models.Cart;
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
    public class CartRepository : ICartRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Add(Cart cart)
        {
            var storedProc = "InsertCart";
            var cartRecord = CartMapper.MapToCartRecord(cart);

            _unitOfWork.Connection.Execute(
                storedProc,
                cartRecord,
                _unitOfWork.Transaction,
                commandType: CommandType.StoredProcedure);

            storedProc = "InsertCartProduct";

            foreach (var cartProduct in cart.Products)
            {
                var cartProductRecord = CartMapper.MapToCartProductRecord(cart.Id, cartProduct);

                _unitOfWork.Connection.Execute(
                    storedProc,
                    cartProductRecord,
                    _unitOfWork.Transaction,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public Cart Find(Guid id)
        {
            var storedProc = "GetCart";
            CartRecord cartRecord;

            cartRecord = _unitOfWork.Connection.Query<CartRecord>(
                storedProc,
                new { id },
                _unitOfWork.Transaction,
                commandType: CommandType.StoredProcedure)
                .FirstOrDefault();

            if (cartRecord == null)
            {
                return null;
            }

            List<CartProductRecord> cartProductRecords;
            storedProc = "GetCartProducts";

            cartProductRecords = _unitOfWork.Connection.Query<CartProductRecord>(
                storedProc,
                new { id },
                _unitOfWork.Transaction,
                commandType: CommandType.StoredProcedure)
                .ToList();

            List<Product> products = new List<Product>();
            storedProc = "GetProduct";

            foreach (var cartProductRecord in cartProductRecords)
            {
                ProductRecord productRecord;

                productRecord = _unitOfWork.Connection.Query<ProductRecord>(
                    storedProc,
                    new { Id = cartProductRecord.ProductId },
                    _unitOfWork.Transaction,
                    commandType: CommandType.StoredProcedure)
                    .FirstOrDefault();

                var product = CartMapper.MapToCartProduct(cartProductRecord, productRecord);
                
                if (product != null)
                {
                    products.Add(product);
                }
            }

            return CartMapper.MapToCart(cartRecord, products);
        }

        public Cart FindByUserId(string userId)
        {
            var storedProc = "GetCartByUserId";
            CartRecord cartRecord;

            cartRecord = _unitOfWork.Connection.Query<CartRecord>(
                storedProc,
                new { userId },
                _unitOfWork.Transaction,
                commandType: CommandType.StoredProcedure)
                .FirstOrDefault();

            if (cartRecord == null)
            {
                return null;
            }

            List<CartProductRecord> cartProductRecords;
            storedProc = "GetCartProducts";

            cartProductRecords = _unitOfWork.Connection.Query<CartProductRecord>(
                storedProc,
                new { cartRecord.Id },
                _unitOfWork.Transaction,
                commandType: CommandType.StoredProcedure)
                .ToList();

            List<Product> products = new List<Product>();
            storedProc = "GetProduct";

            foreach (var cartProductRecord in cartProductRecords)
            {
                ProductRecord productRecord;

                productRecord = _unitOfWork.Connection.Query<ProductRecord>(
                    storedProc,
                    new { Id = cartProductRecord.ProductId },
                    _unitOfWork.Transaction,
                    commandType: CommandType.StoredProcedure)
                    .FirstOrDefault();

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
            var storedProc = "UpdateCart";

            _unitOfWork.Connection.Execute(storedProc,
                new { CartId = cart.Id, Purchased = cart.Purchased },
                _unitOfWork.Transaction,
                commandType: CommandType.StoredProcedure);

            storedProc = "DeleteCartProducts";

            _unitOfWork.Connection.Execute(storedProc,
                new { cart.Id },
                _unitOfWork.Transaction,
                commandType: CommandType.StoredProcedure);

            storedProc = "InsertCartProduct";

            foreach (var cartProduct in cart.Products)
            {
                var cartProductRecord = CartMapper.MapToCartProductRecord(cart.Id, cartProduct);

                _unitOfWork.Connection.Execute(storedProc,
                    cartProductRecord,
                    _unitOfWork.Transaction,
                    commandType: CommandType.StoredProcedure);
            }
        }
    }
}
