using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthwindModel;
using dataservice.Connected_Services.Employee_Service;

namespace dataservice.Controllers
{
    [Produces("application/json")]
    [Route("api/Employees")]
    public class EmployeesController : Controller
    {
        // GET: api/Employees
        [HttpGet]
        public async Task<List<Employee>> Get()
        {
            return await DataAccess.GetEmployees();
        }

        // GET: api/Employees/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<Employee> Get(int id)
        {
            return await DataAccess.GetEmployeeByID(id);
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
