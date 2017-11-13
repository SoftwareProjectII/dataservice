using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NorthwindModel;

namespace dataservice.Controllers
{
    [Produces("application/json")]
    [Route("api/Employees")]
    public class EmployeesController : Controller
    {
        // GET: api/Employees
        [HttpGet]
        public async Task<Dictionary<int, Employee>> Get()
        {
            return await DataAccess.GetEmployees();
        }

        // GET: api/Employees/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<Employee> Get(int id)
        {
            return await DataAccess.GetEmployeeByID(id);
        }

        [HttpGet("m{id}", Name = "GetByManager")]
        public async Task<Dictionary<int, Employee>> GetByReportsTo(int id)
        {
            return await DataAccess.GetEmployeesByReportsTo(id);
        }

        // POST: api/Employees
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT: api/Employees/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
