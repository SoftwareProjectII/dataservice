using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dataservice.Models;
using dataservice.Code;
using NorthwindModel;

namespace dataservice.Controllers
{
    [Produces("application/json")]
    [Route("api/Statistics")]
    public class StatisticsController : Controller
    {
        private readonly _17SP2G4Context _context;
        private readonly EmployeeProvider employeeProvider;

        public StatisticsController(_17SP2G4Context context, EmployeeProvider employeeProvider)
        {
            _context = context;
            this.employeeProvider = employeeProvider;
        }

        // GET: api/Statistics/5
        [HttpGet("{year}")]
        public async Task<IActionResult> GetUser([FromRoute] int year)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<EmployeeTrainingCountWrapper> utc = new List<EmployeeTrainingCountWrapper>();

            var users = await _context.User.ToListAsync();

            EmployeeTrainingCountWrapper highestCount = null;

            foreach (User user in users)
            {
                var count = await _context
                    .Followingtraining
                    .Where(f => f.UserId == user.UserId 
                    && f.TrainingSession.Date.Year == year 
                    && f.TrainingSession.Date < DateTime.Now 
                    && !f.TrainingSession.Cancelled 
                    && f.IsApproved 
                    && !f.IsCancelled
                    && !f.IsDeclined)
                    .CountAsync();
                if (count == 0)
                {
                    continue;
                }
                EmployeeTrainingCountWrapper wrapper = new EmployeeTrainingCountWrapper();

                if (!user.EmpId.HasValue)
                {
                    continue;
                }
                wrapper.employee = employeeProvider.Employees[user.EmpId.Value];
                wrapper.Count = count;

                if (highestCount == null)
                {
                    utc.Add(wrapper);
                    highestCount = wrapper;
                }
                else if (wrapper.Count > highestCount.Count)
                {
                    utc.Clear();
                    utc.Add(wrapper);
                    highestCount = wrapper;
                }
                else if(wrapper.Count == highestCount.Count)
                {
                    utc.Add(wrapper);
                }
            }

            return Ok(utc);
        }       
    }

    public class EmployeeTrainingCountWrapper
    {
        public Employee employee { get; set; }
        public int Count { get; set; }
    }
}