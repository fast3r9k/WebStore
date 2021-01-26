using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Entities;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;

namespace WebStore.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly IEmployeesData _Employees;
        public EmployeesController(IEmployeesData Employees) => _Employees = Employees;
        public IActionResult Index()
        {
            var emp = _Employees.Get();
            return View(emp);
        }
        public IActionResult More(int id)
        {
            var emp = _Employees.Get(id);
            if (emp is not null)
                return View(emp);
            return RedirectToAction("Error404", "Home");

        }

        [Authorize(Roles = Role.Admin)]
        public IActionResult Create() => View("Edit", new EmployeesViewModel());

        #region Edit
        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            //editing implementation
            if (id is null)
                return View(new EmployeesViewModel());

            if (id < 0)
                return BadRequest();

            var emp = _Employees.Get((int)id);

            if (emp is null)
                return RedirectToAction("Error404", "Home");


            return View(new EmployeesViewModel
            {
                Id = emp.Id,
                LastName = emp.LastName,
                FirstName = emp.FirstName,
                Patronymic = emp.Patronymic,
                Age = emp.Age
            });
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost]
        public IActionResult Edit(EmployeesViewModel model)
        {
            if (model is null) throw new ArgumentNullException(nameof(model));
            if (ModelState.IsValid)
            {
                var emp = new Employee
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Patronymic = model.Patronymic,
                    Age = model.Age,
                    Id = model.Id
                };

                if (emp.Id == 0)
                    _Employees.Add(emp);
                else
                    _Employees.Update(emp);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        #endregion
        #region Delete
        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id <= 0) return BadRequest();

            var emp = _Employees.Get(id);

            if (emp is null) return NotFound();

            return View(new EmployeesViewModel
            {
                Id = emp.Id,
                LastName = emp.LastName,
                FirstName = emp.FirstName,
                Age = emp.Age,
                Patronymic = emp.Patronymic
            });
        }

        [Authorize(Roles = Role.Admin)]
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            _Employees.Delete(id);
            return RedirectToAction("Index");
        }
        #endregion

    }
}