using Domain.Models.Order;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IOrderRepository
    {
        public Task Add(Order order);
        public Task<Order> Find(Guid id);
        public Task<List<Order>> GetAll();
        public Task<List<Order>> GetUsersOrders(string userID);
    }
}
