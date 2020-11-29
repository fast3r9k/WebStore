using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _Configuration;
        private static readonly List<Employee> __Employees = new()
        {
            new Employee {Id=1, LastName="Антонов",FirstName="Антон",Patronymic="Антонович",Age=20},
            new Employee {Id=2, LastName="Андреев",FirstName="Андрей",Patronymic="Андреевич",Age=40},
            new Employee {Id=3, LastName="Никитин",FirstName="Никита",Patronymic="Никитич",Age=30},
        };
        public HomeController(IConfiguration Configuration) => _Configuration = Configuration;

        public IActionResult Index() => View();
        public IActionResult SecondAction()
        {
            return Content("Second action");
        }

        public IActionResult Employees()
        {
            return View(__Employees);
        }   
        
        public IActionResult More(int id)
        {

            return View(__Employees.Where(e => e.Id == id).First());
        }

    }
}
