using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;

namespace WebStore.Data
{
    public class TestData
    {
        public static  List<Employee> __Employees = new()
        {
            new Employee { Id = 1, LastName = "Антонов", FirstName = "Антон", Patronymic = "Антонович", Age = 20 },
            new Employee { Id = 2, LastName = "Андреев", FirstName = "Андрей", Patronymic = "Андреевич", Age = 40 },
            new Employee { Id = 3, LastName = "Никитин", FirstName = "Никита", Patronymic = "Никитич", Age = 30 },
        };
    }
}
