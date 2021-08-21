using Domain.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IOrderRepository
    {
        public void Add(Order order);
        public Order? Find(Guid id);
        public List<Order> GetAll();
    }
}
