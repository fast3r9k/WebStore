using System.Collections.Generic;
using System.Threading.Tasks;
using WebStore.Domain.DTO.Orders;
using WebStore.Domain.Entities;
using WebStore.Domain.ViewModels;

namespace WebStore.Interfaces.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDTO>> GetUsersOrders(string UserName);

        Task<OrderDTO> GetOrderById(int id);

        Task<OrderDTO> CreateOrder(string UserName, CreateOrderModel OrderModel);
    }
}
