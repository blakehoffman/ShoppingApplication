using Application.DTO;
using Application.DTO.Order;
using System.Collections.Generic;

namespace Application.Services.Interfaces
{
    public interface IOrderService
    {
        public ResultDTO CreateOrder(string userID, CreateOrderDTO createOrderDTO);
        public List<OrderDTO> GetOrders();
        public List<OrderDTO> GetOrders(string userID);
    }
}
