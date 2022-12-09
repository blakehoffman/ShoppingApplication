using Application.DTO;
using Application.DTO.Order;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IOrderService
    {
        public Task<ResultDTO> CreateOrder(string userID, CreateOrderDTO createOrderDTO);
        public Task<List<OrderDTO>> GetOrders();
        public Task<List<OrderDTO>> GetOrders(string userID);
    }
}
