using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NorthwindModel;
using System.Data.Services.Client;
using dataservice.Connected_Services.Employee_Service;

namespace dataservice.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        NorthwindEntities context = new NorthwindEntities(new Uri("http://services.odata.org/V3/Northwind/Northwind.svc"));
        // GET api/values
        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            return DataAccess.Employees;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<string> Get(int id)
        {
            IEnumerable<Employee> test = await context.Employees.ExecuteAsync();
            return test.Where(x => x.EmployeeID == id).FirstOrDefault().FirstName;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
