using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NorthwindModel;
using System.Linq;

namespace dataservice.Controllers
{
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

        [HttpGet("test")]
        public ActionResult Test()
        {
            return Ok(employeeProvider.Users);
        }

        // GET: api/Employees/5
        [HttpGet("{id}", Name = "Get")]
        public Employee Get(int id)
        {
            return employeeProvider.Employees.Where(m => m.Value.EmployeeID == id).FirstOrDefault().Value;
        }

        // GET: api/employees/5/manages
        [HttpGet("{id}/manages")]
        public Dictionary<int, Employee> GetByReportsTo(int id)
        {
            return employeeProvider.Employees.Where(m => m.Value.ReportsTo == id).ToDictionary(m => m.Key, m => m.Value);
        }
    }
}
