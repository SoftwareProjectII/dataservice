using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dataservice.Models;
using NorthwindModel;
using System.Security.Cryptography;
using System;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

// Hashing: https://docs.microsoft.com/en-us/aspnet/core/security/data-protection/consumer-apis/password-hashing

namespace dataservice.Controllers
{
    [Produces("application/json")]
    [Route("api/Users")]
    public class UsersController : Controller
    {
        private readonly _17SP2G4Context _context;
        private readonly EmployeeProvider employeeProvider;

        public UsersController(_17SP2G4Context context, EmployeeProvider employeeProvider)
        {
            _context = context;
            this.employeeProvider = employeeProvider;
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

        // GET: api/users/load
        [HttpGet("load")]
        public async Task<IActionResult> LoadUsers([FromRoute] int id)
        {
            List<int?> emps = await _context.User.Where(m => m.EmpId != null).Select(m => m.EmpId).ToListAsync();
            Dictionary<int, Employee> Employees = employeeProvider.Employees.Where(m => !emps.Contains(m.Value.EmployeeID))
                .ToDictionary(m => m.Key, m => m.Value);

            foreach (Employee e in Employees.Values)
            {
                User user = new User();
                user.Username = e.FirstName[0] + e.LastName;
                user.EmpId = e.EmployeeID;
                user.Salt = getSalt();
                user.Password = HashPassword(e.EmployeeID.ToString() + e.LastName, user.Salt);

                _context.User.Add(user);
            }
            await _context.SaveChangesAsync();

            return Ok();
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

        // GET: api/users/5/trainingsessions
        [HttpGet("{id}/trainingsessions")]
        public async Task<IActionResult> GetTrainingSessions([FromRoute] int id)
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

        // POST: api/Users/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginWrapper login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string username = login.username;
            string password = login.password;

            User user = await _context.User.Where(m => m.Username == username).FirstOrDefaultAsync();

            if (user == null || user.Password != password)
            {
                return Unauthorized();
            }

            else return Ok(user);
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

        private async void InitUsers(Dictionary<int, Employee> employees)
        {
            foreach (Employee e in employees.Values)
            {
                User user = new User();
                user.Username = e.FirstName[0] + e.LastName;
                user.EmpId = e.EmployeeID;
                user.Salt = getSalt();
                user.Password = HashPassword(e.EmployeeID.ToString() + e.LastName, user.Salt);

                _context.User.Add(user);
            }
            await _context.SaveChangesAsync();
        }

        private string HashPassword(string password, byte[] salt)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 1000,
            numBytesRequested: 256 / 8));
            return hashed;
        }

        private byte[] getSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }
    }
}

public class LoginWrapper
{
    public string username { get; set; }
    public string password { get; set; }
}