using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NorthwindModel;
using System.Linq;
using dataservice.Code;
using Microsoft.AspNetCore.Authorization;
using dataservice.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;

namespace dataservice.Controllers
{
    [Authorize]
    [AllowAnonymous]
    [Produces("application/json")]
    [Route("api/Employees")]
    public class EmployeesController : Controller
    {
        private readonly _17SP2G4Context _context;
        private readonly EmployeeProvider employeeProvider;

        public EmployeesController(EmployeeProvider employeeProvider, _17SP2G4Context context)
        {
            this.employeeProvider = employeeProvider;
            _context = context;
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

        // GET: api/employees/5/manages/trainings
        [HttpGet("{id}/manages/trainings")]
        public async Task<IActionResult> GetManagedUsersTrainings([FromRoute] int id, bool? future)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.User.FirstOrDefaultAsync(m => m.UserId == id);

            if (user == null)
            {
                return NotFound("User not found");
            }

            if (!user.EmpId.HasValue)
            {
                return BadRequest("User is not an employee");
            }

            List<int> manages;

            try
            {
                manages = employeeProvider.Employees.Where(m => m.Value.ReportsTo == user.EmpId).Select(e => e.Key).ToList();
            }
            catch (ArgumentNullException ex)
            {
                return NotFound("Employee doesn't manage anyone");
            }

            List<Followingtraining> followingtrainings;

            if (future.HasValue)
            {
                if (future.Value)
                {
                    followingtrainings = await _context.Followingtraining
                        .Include(f => f.TrainingSession).ThenInclude(t => t.Training)
                        .Include(f => f.TrainingSession).ThenInclude(t => t.Address)
                        .Include(f => f.User)
                        .Where(f => f.User.EmpId.HasValue && manages.Contains(f.User.EmpId.GetValueOrDefault()) && f.TrainingSession.Date.Add(f.TrainingSession.StartHour) > DateTime.Now)
                        .ToListAsync();
                }
                else
                {
                    followingtrainings = await _context.Followingtraining
                        .Include(f => f.TrainingSession).ThenInclude(t => t.Training)
                        .Include(f => f.TrainingSession).ThenInclude(t => t.Address)
                        .Include(f => f.User)
                        .Where(f => f.User.EmpId.HasValue && manages.Contains(f.User.EmpId.GetValueOrDefault()) && f.TrainingSession.Date.Add(f.TrainingSession.StartHour) < DateTime.Now)
                        .ToListAsync();
                }
            }

            else
            {
                followingtrainings = await _context.Followingtraining
                        .Include(f => f.TrainingSession).ThenInclude(t => t.Training)
                        .Include(f => f.TrainingSession).ThenInclude(t => t.Address)
                        .Include(f => f.User)
                        .Where(f => f.User.EmpId.HasValue && manages.Contains(f.User.EmpId.GetValueOrDefault())).ToListAsync();
            }

            

            List<followingTrainingWrapper> fts = new List<followingTrainingWrapper>();

            foreach (var ft in followingtrainings)
            {
                followingTrainingWrapper tmp = new followingTrainingWrapper();
                tmp.followingtraining = ft;
                tmp.fName = employeeProvider.Employees.Where(e => e.Key == ft.User.EmpId).Select(e => e.Value.FirstName).FirstOrDefault();
                tmp.lName = employeeProvider.Employees.Where(e => e.Key == ft.User.EmpId).Select(e => e.Value.LastName).FirstOrDefault();
                fts.Add(tmp);
            }

            if (followingtrainings == null)
            {
                return NotFound();
            }


            return Ok(fts);
        }
    }
    class followingTrainingWrapper
    {
        public Followingtraining followingtraining { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
    }
}
