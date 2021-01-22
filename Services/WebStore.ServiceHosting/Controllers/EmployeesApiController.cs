using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebStore.Domain.Models;
using WebStore.Interfaces.Services;

namespace WebStore.ServiceHosting.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/employees")]

    [ApiController]
    public class EmployeesApiController : ControllerBase, IEmployeesData
    {
        private readonly IEmployeesData _EmployeesData;
        private readonly ILogger<EmployeesApiController> _Logger;

        public EmployeesApiController(IEmployeesData EmployeesData, ILogger<EmployeesApiController> Logger)
        {
            _EmployeesData = EmployeesData;
            _Logger = Logger;
        }

        [HttpGet]
        public IEnumerable<Employee> Get() => _EmployeesData.Get();

        [HttpGet("{id}")]
        public Employee Get(int id) => _EmployeesData.Get(id);

        [HttpPost]
        public int Add(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                _Logger.LogWarning($"Data model error when adding an employee {employee.LastName} {employee.FirstName} {employee.Patronymic}");
                return 0;
            }
            _Logger.LogInformation($"Add employee {employee.LastName} {employee.FirstName} {employee.Patronymic}");
            var id = _EmployeesData.Add(employee);
            if (id > 0)
                _Logger.LogInformation($"Employee with id:{employee.Id} {employee.LastName} {employee.FirstName} {employee.Patronymic} has been added");
            else
                _Logger.LogWarning($"Error adding employee {employee.Id} {employee.LastName} {employee.FirstName} {employee.Patronymic}");
            return id;
        }

        [HttpPut]
        public void Update(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                _Logger.LogWarning($"Data model error when editing an employee {employee.LastName} {employee.FirstName} {employee.Patronymic}");
            } 
            _Logger.LogInformation($"Edit employee {employee.LastName} {employee.FirstName} {employee.Patronymic}");
            _EmployeesData.Update(employee);
        }


        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            var employee = _EmployeesData.Get(id);
            _Logger.LogInformation($"Delete employee {employee.Id} {employee.LastName} {employee.FirstName} {employee.Patronymic}");
            if (id > 0)
                _Logger.LogInformation($"Employee with id:{employee.Id} {employee.LastName} {employee.FirstName} {employee.Patronymic} has been deleted");
            else
                _Logger.LogWarning($"Error deleting employee {employee.Id} {employee.LastName} {employee.FirstName} {employee.Patronymic}");

            return _EmployeesData.Delete(id);
        }
    }
}
