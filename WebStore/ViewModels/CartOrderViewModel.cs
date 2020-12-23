using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Entities;

namespace WebStore.ViewModels
{
    public class CartOrderViewModel
    {
        public  CartViewModel Cart { get; set; }

        public OrderViewModel Order { get; set; }
    }
}
