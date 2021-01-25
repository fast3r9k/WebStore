﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebStore.Clients.Base;
using WebStore.Domain.DTO.Orders;
using WebStore.Domain.Entities.Identity;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;

namespace WebStore.Clients.Orders
{
    public class OrdersClient : BaseClient, IOrderService
    {
        private readonly ILogger<OrdersClient> _Logger;
        public OrdersClient(IConfiguration Configuration, ILogger<OrdersClient> Logger) : base(Configuration, WebApi.Orders) => _Logger = Logger;

        public async Task<IEnumerable<OrderDTO>> GetUsersOrders(string UserName) => await
            GetAsync<IEnumerable<OrderDTO>>($"{Address}/user/{UserName}");

        public async Task<OrderDTO> GetOrderById(int id) => await GetAsync<OrderDTO>($"{Address}/{id}");

        public async Task<OrderDTO> CreateOrder(string UserName, CreateOrderModel OrderModel)
        {
            var response = await PostAsync($"{Address}/{UserName}", OrderModel);
            return await response.Content.ReadAsAsync<OrderDTO>();
        }
    }
}
