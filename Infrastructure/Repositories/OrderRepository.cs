using Domain.Models.Order;
using Domain.Repositories;
using Domain.UnitOfWork;
using Infrastructure.Contexts;
using Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IUnitOfWork _unitOfWork;

        public OrderRepository(ApplicationDbContext applicationDbContext, IUnitOfWork unitOfWork)
        {
            _applicationDbContext = applicationDbContext;
            _unitOfWork = unitOfWork;
        }

        public async Task Add(Order order)
        {
            var orderEntity = OrderMapper.MapToOrderEntity(order);
            orderEntity.Products = new();

            foreach (var orderProduct in order.Products)
            {
                var orderProductEntity = OrderMapper.MapProductToOrderProductEntity(orderProduct, order.Id);
                orderEntity.Products.Add(orderProductEntity);
            }

            _applicationDbContext.Orders.Add(orderEntity);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<Order> Find(Guid id)
        {
            var orderEntity = await _applicationDbContext.Orders
                .Include(order => order.Products)
                .ThenInclude(orderProduct => orderProduct.Product)
                .Include(order => order.Discount)
                .AsNoTracking()
                .SingleOrDefaultAsync(order => order.Id == id);

            return OrderMapper.MapOrderEntityToOrder(orderEntity);
        }

        public async Task<List<Order>> GetAll()
        {
            var orderEntities = await _applicationDbContext.Orders
                .Include(order => order.Products)
                .ThenInclude(orderProduct => orderProduct.Product)
                .Include(order => order.Discount)
                .AsNoTracking()
                .ToListAsync();

            var orders = new List<Order>();

            foreach (var orderEntity in orderEntities)
            {
                orders.Add(OrderMapper.MapOrderEntityToOrder(orderEntity));
            }

            return orders;
        }

        public async Task<List<Order>> GetUsersOrders(string userID)
        {
            var orderEntities = await _applicationDbContext.Orders
                .Include(order => order.Products)
                .ThenInclude(orderProduct => orderProduct.Product)
                .Include(order => order.Discount)
                .Where(order => order.UserId == new Guid(userID))
                .AsNoTracking()
                .ToListAsync();

            var orders = new List<Order>();

            foreach (var orderEntity in orderEntities)
            {
                orders.Add(OrderMapper.MapOrderEntityToOrder(orderEntity));
            }

            return orders;
        }
    }
}
