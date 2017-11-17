using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dataservice.Models;
using NorthwindModel;

namespace dataservice.Controllers
{
    [Produces("application/json")]
    [Route("api/Users")]
    public class UsersController : Controller
    {
        private readonly _17SP2G4Context _context;

        public UsersController(_17SP2G4Context context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public IEnumerable<User> GetUser()
        {
            return _context.User;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.User.SingleOrDefaultAsync(m => m.UserId == id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }


        [HttpGet("load")]
        public async Task<IActionResult> LoadUsers([FromRoute] int id)
        {
            List<int> users = await _context.User.Select(m => m.UserId).ToListAsync();
            Dictionary<int, Employee> employees = await DataAccess.GetEmployees();

            if (users.Count == 0)
            {
                InitUsers(false);
            }

            if (users.Count < employees.Keys.Count())
            {
                InitUsers(true, employees);
            }

            return Ok();
        }

        private async void InitUsers(bool forUpdate, Dictionary<int, Employee> employees = null)
        {
            if (forUpdate == false)
            {
                foreach (Employee e in employees.Values)
                {
                    User user = new User();
                    user.UserId = e.EmployeeID;
                    user.Email = "NOT_SET";
                    user.Password = e.EmployeeID.ToString() + e.FirstName + e.LastName;
                    _context.User.Add(user);
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
            }
        }


        // GET: api/users/5/certificates
        [HttpGet("{id}/certificates")]
        public async Task<IActionResult> GetCertificates([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var certificates = await _context.Usercertificate.Where(m => m.UserId == id).Select(m => m.Certificate).ToListAsync();

            if (certificates == null)
            {
                return NotFound();
            }

            return Ok(certificates);
        }

        // GET: api/users/5/trainings
        [HttpGet("{id}/trainings")]
        public async Task<IActionResult> GetTrainings([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trainings = await _context.Followingtraining.Where(m => m.UserId == id).Select(m => m.TrainingSession).ToListAsync();

            if (trainings == null)
            {
                return NotFound();
            }

            return Ok(trainings);
        }

        // GET: api/users/5/surveyanswers
        [HttpGet("{id}/surveyanswers")]
        public async Task<IActionResult> GetSurveyanswers([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var answers = await _context.Surveyanswer.Where(m => m.UserId == id).ToListAsync();

            if (answers == null)
            {
                return NotFound();
            }

            return Ok(answers);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser([FromRoute] int id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.User.SingleOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.UserId == id);
        }
    }
}