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
        #region Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null) return NotFound();

            if (id <= 0) return BadRequest();

            var emp = TestData.__Employees.Where(e => e.Id == id).First();

            if (emp != null)
            {                
                return View(emp);
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult Edit(Employee emp, int id)
        {
            //editing implementation
            _Employees[id] = emp;
            return RedirectToAction(nameof(Index));
        }
        #endregion
        #region Delete
        public IActionResult Delete(int? id)
        {
            if (id is null) return NotFound();

            if (id <= 0) return BadRequest();

            var emp = TestData.__Employees.Where(e => e.Id == id).First();

            if (emp != null)
            {
                return View(emp);
            }
            return NotFound();
        }
        [HttpPost]
        public IActionResult Delete(Employee emp)
        {
            TestData.__Employees.Remove(emp);
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
