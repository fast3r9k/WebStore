using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.ViewModels
{
    public record UserOrderViewModel(int Id, string Name, string Phone, string Address, decimal TotalSum);

}
