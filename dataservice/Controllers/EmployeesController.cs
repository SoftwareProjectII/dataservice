using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NorthwindModel;
using System.Linq;
using dataservice.Code;
using Microsoft.AspNetCore.Authorization;

namespace dataservice.Controllers
{    
    [Authorize]
    [Produces("application/json")]
    [Route("api/Employees")]
    public class EmployeesController : Controller
    {
        private readonly EmployeeProvider employeeProvider;

        public EmployeesController(EmployeeProvider employeeProvider)
        {
            this.employeeProvider = employeeProvider;
        }

        // GET: api/Employees
        [HttpGet]
        public Dictionary<int, Employee> Get()
        {
            return employeeProvider.Employees;
        }        

        // GET: api/Employees/5
        [HttpGet("{id}", Name = "Employee_Single")]
        public Employee Get(int id)
        {
            return employeeProvider.Employees.Where(m => m.Value.EmployeeID == id).FirstOrDefault().Value;
        }

        // GET: api/employees/5/manages
        [HttpGet("{id}/manages", Name = "Employee_Manages")]
        public Dictionary<int, Employee> GetByReportsTo(int id)
        {
            return employeeProvider.Employees.Where(m => m.Value.ReportsTo == id).ToDictionary(m => m.Key, m => m.Value);
        }
    }
}
