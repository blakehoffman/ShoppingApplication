using Application.DTO;
using Application.DTO.Order;
using Application.Services.Interfaces;
using AutoMapper;
using Domain.Models.Order;
using Domain.Repositories;
using System;
using System.Collections.Generic;

namespace Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public OrderService(
            ICartRepository cartRepository,
            IDiscountRepository discountRepository,
            IMapper mapper,
            IOrderRepository orderRepository,
            IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _discountRepository = discountRepository;
            _mapper = mapper;
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public ResultDTO CreateOrder(string userID, CreateOrderDTO createOrderDTO)
        {
            var resultDTO = new ResultDTO();

            if (string.IsNullOrWhiteSpace(userID))
            {
                resultDTO.Errors.Add("User ID cannot be empty");
            }

            if (createOrderDTO == null)
            {
                resultDTO.Errors.Add("Create order object cannot be null");
                resultDTO.Succeeded = false;

                return resultDTO;
            }

            if (createOrderDTO.Products == null || createOrderDTO.Products.Count < 1)
            {
                resultDTO.Errors.Add("In order to create an order, there must be at least one product attached to it");
                resultDTO.Succeeded = false;

                return resultDTO;
            }

            var discount = _discountRepository.FindByCode(createOrderDTO.Discount);

            if (!string.IsNullOrWhiteSpace(createOrderDTO.Discount) && discount == null)
            {
                resultDTO.Errors.Add("Discount doesn't exist");
            }

            foreach (var product in createOrderDTO.Products)
            {
                if (_productRepository.Find(product.Id) == null)
                {
                    resultDTO.Errors.Add($"Product {product.Id} does not exist");
                }
            }

            if (resultDTO.Errors.Count > 0)
            {
                resultDTO.Succeeded = false;
                return resultDTO;
            }

            var orderDiscount = discount == null ? null : new Discount(discount.Id, discount.Amount);
            var order = new Order(Guid.NewGuid(), userID, DateTimeOffset.UtcNow, orderDiscount);

            foreach (var productDTO in createOrderDTO.Products)
            {
                var product = _productRepository.Find(productDTO.Id);
                var orderProduct = new Product(product.Id, product.Name, product.Price, productDTO.Quantity);

                order.AddItem(orderProduct);
            }

            _orderRepository.Add(order);
            var cart = _cartRepository.FindByUserId(userID);

            if (cart != null)
            {
                cart.Purchased = true;
                _cartRepository.Update(cart);
            }

            resultDTO.Succeeded = true;
            return resultDTO;
        }

        public List<OrderDTO> GetOrders()
        {
            var orders = _orderRepository.GetAll();
            return _mapper.Map<List<OrderDTO>>(orders);
        }

        public List<OrderDTO> GetOrders(string userID)
        {
            var orders = _orderRepository.GetUsersOrders(userID);
            return _mapper.Map<List<OrderDTO>>(orders);
        }
    }
}
