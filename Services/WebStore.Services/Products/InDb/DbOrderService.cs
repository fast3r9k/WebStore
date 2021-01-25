using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain.DTO.Orders;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;

namespace WebStore.Services.Products.InDb
{
    public class DbOrderService : IOrderService
    {
        private readonly WebStoreDb _db;
        private readonly UserManager<User> _UserManager;

        public DbOrderService(WebStoreDb db, UserManager<User> UserManager)
        {
            _db = db;
            _UserManager = UserManager;
        }

        public async Task<IEnumerable<OrderDTO>> GetUsersOrders(string UserName) => (await _db.Orders
               .Include(order => order.User)
               .Include(order => order.Items)
               .Where(order => order.User.UserName == UserName)
               .ToArrayAsync())
               .Select(o => o.ToDTO());

        public async Task<OrderDTO> GetOrderById(int id) => (await _db.Orders
           .Include(order => order.User)
           .Include(order => order.Items)
           .FirstOrDefaultAsync(order => order.Id == id))
           .ToDTO(); 

        public async Task<OrderDTO> CreateOrder(string UserName, CreateOrderModel OrderModel)
        {
            var user = await _UserManager.FindByNameAsync(UserName);
            if (user is null)
                throw new InvalidOperationException($"User {UserName} is not found in DB");
            await using var transaction = await _db.Database.BeginTransactionAsync();

            var order = new Order
            {
                Name = OrderModel.Order.Name,
                Address = OrderModel.Order.Address,
                Phone = OrderModel.Order.Phone,
                User = user,
                Date = DateTime.Now
            };

            foreach (var (id,_,quantity) in OrderModel.Items)
            {
                var product = await _db.Products.FindAsync(id);
                if(product is null)
                    continue;
                var orderItem = new OrderItem
                {
                    Order = order,
                    TotalPrice = product.Price,
                    Quantity = quantity,
                    Product = product
                };

                order.Items.Add(orderItem);
            }

            await _db.Orders.AddAsync(order);

            await _db.SaveChangesAsync();

            await transaction.CommitAsync();

            return order.ToDTO();


        }
    }
}
