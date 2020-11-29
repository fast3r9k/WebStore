using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Data;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class EmployeesController : Controller
    {
        private List<Employee> _Employees;
        public EmployeesController()=> _Employees = TestData.__Employees;
        public IActionResult Index() => View(_Employees);
        public IActionResult More(int id)
        {
            var emp = TestData.__Employees.Where(e => e.Id == id).First();
            if(emp is not null)
                return View(emp);
            return NotFound();

        }
    }
}
