﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebStore.Domain.Entities
{
    public class Cart
    {
        public  ICollection<CartItem> Items { get; set; }
        public int ItemsCount => Items?.Sum(item => item.Quantity) ?? 0;
    }

    public class CartItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
