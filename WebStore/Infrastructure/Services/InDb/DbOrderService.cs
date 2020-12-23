using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Identity;
using WebStore.Infrastructure.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Infrastructure.Services.InDb
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

        public async Task<IEnumerable<Order>> GetUsersOrders(string UserName) => await _db.Orders
               .Include(order => order.User)
               .Include(order => order.Items)
               .Where(order => order.User.UserName == UserName)
               .ToArrayAsync();

        public async Task<Order> GetOrderById(int id) => await _db.Orders
           .Include(order => order.User)
           .Include(order => order.Items)
           .FirstOrDefaultAsync(order => order.Id == id);

        public async Task<Order> CreateOrder(string UserName, CartViewModel Cart, OrderViewModel OrderModel)
        {
            var user = await _UserManager.FindByNameAsync(UserName);
            if (user is null)
                throw new InvalidOperationException($"User {UserName} is not found in DB");
            await using var transaction = await _db.Database.BeginTransactionAsync();

            var order = new Order
            {
                Name = OrderModel.Name,
                Address = OrderModel.Address,
                Phone = OrderModel.Phone,
                User = user,
                Date = DateTime.Now
            };

            foreach (var (productModel, quantity) in Cart.Items)
            {
                var product = await _db.Products.FindAsync(productModel.Id);
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

            return order;


        }
    }
}
