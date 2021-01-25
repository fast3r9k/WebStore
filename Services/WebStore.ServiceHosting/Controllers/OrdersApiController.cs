using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebStore.Domain.DTO.Orders;
using WebStore.Domain.Entities;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{
    [Route(WebApi.Orders)]
    [ApiController]
    public class OrdersApiController : ControllerBase, IOrderService
    {
        private readonly IOrderService _OrderService;
        private readonly ILogger _Logger;

        public OrdersApiController(IOrderService OrderService, ILogger<OrdersApiController> Logger)
        {
            _OrderService = OrderService;
            _Logger = Logger;
        }

        public async Task<IEnumerable<OrderDTO>> GetUsersOrders(string UserName) => await _OrderService.GetUsersOrders(UserName);

        public async Task<OrderDTO> GetOrderById(int id) => await _OrderService.GetOrderById(id);

        public async Task<OrderDTO> CreateOrder(string UserName, [FromBody] CreateOrderModel OrderModel)
        {
        
            _Logger.LogInformation($"Make order for User {UserName}");
            var order = await _OrderService.CreateOrder(UserName, OrderModel);
            return order;

        } 
    }
}
