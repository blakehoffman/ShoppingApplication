using Domain.Models.Order;
using System;
using System.Collections.Generic;

namespace Domain.Repositories
{
    public interface IOrderRepository
    {
        public void Add(Order order);
        public Order Find(Guid id);
        public List<Order> GetAll();
        public List<Order> GetUsersOrders(string userID);
    }
}
