using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebStore.Domain.DTO.Orders;
using WebStore.Domain.Entities;

namespace WebStore.Services.Mapping
{
    public static class OrderMapper
    {
        public static OrderItemDTO ToDTO(this OrderItem Item) => Item is null
            ? null
            : new OrderItemDTO(Item.Id, Item.TotalPrice, Item.Quantity);

        public static OrderItem FromDTO(this OrderItemDTO Item) => Item is null
            ? null
            : new OrderItem
            {
                Id = Item.Id,
                TotalPrice = Item.TotalPrice,
                Quantity = Item.Quantity
            };

        public static OrderDTO ToDTO(this Order Item) => Item is null
            ? null
            : new OrderDTO(Item.Id, Item.Name, Item.Phone, Item.Address, Item.Date, Item.Items.Select(ToDTO));

        public static Order FromDTO(this OrderDTO Item) => Item is null
            ? null
            : new Order
            {
                Id = Item.Id,
                Name = Item.Name,
                Phone = Item.Phone,
                Address = Item.Address,
                Date = Item.Date,
                Items = Item.Items.Select(FromDTO).ToList()
            };

    }
}
