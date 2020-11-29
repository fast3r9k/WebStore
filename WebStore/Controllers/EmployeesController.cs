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
        public IActionResult Edit(Employee emp)
        {
            //editing implementation

            Employee currentEmp = TestData.__Employees.Where(e => emp.Id == e.Id).First();
            TestData.__Employees.Remove(currentEmp);
            TestData.__Employees.Add(emp);
            TestData.__Employees = TestData.__Employees.OrderBy(e => e.Id).ToList();

            return RedirectToAction(nameof(Index));
        }
        #endregion

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Employee toDelete = TestData.__Employees.Where(e => e.Id == id).FirstOrDefault();
            if (Object.Equals(toDelete, null))
                return NotFound();
            TestData.__Employees.Remove(toDelete);
            return RedirectToAction("Index");
        }

    }
}
