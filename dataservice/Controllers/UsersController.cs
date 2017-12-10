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
using dataservice.Code;
using Microsoft.AspNetCore.Authorization;

// Hashing: https://docs.microsoft.com/en-us/aspnet/core/security/data-protection/consumer-apis/password-hashing

namespace dataservice.Controllers
{
    [Authorize]
    [AllowAnonymous]
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

        // GET: api/users/5/survey/questions?surveyid=1
        [HttpGet("{id}/survey/questions")]
        public async Task<IActionResult> GetSurveyanswers([FromRoute] int id, int? surveyid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<Surveyquestion> questions;

            if (surveyid.HasValue)
            {
                //var answers = await _context.Surveyanswer.Where(u => u.UserId == id).Select(a => a.QuestionId).ToListAsync();

                questions = await _context.Surveyquestion.
                    Where(q => q.SurveyId == surveyid && q.Surveyanswer.FirstOrDefault(a => a.UserId == id) == null).
                    ToListAsync();
            }
            else
            {
                questions = await _context.Surveyquestion.
                   Where(q => q.Surveyanswer.FirstOrDefault(a => a.UserId == id) == null).
                   ToListAsync();
            }

            //var answers = await _context.Surveyanswer.Where(m => m.UserId == id).ToListAsync();

            if (questions == null)
            {
                return NotFound();
            }

            return Ok(questions);
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

        // POST: api/Users/salt
        [AllowAnonymous]
        [HttpGet("{username}/salt")]
        public async Task<IActionResult> GetUserSalt([FromRoute] string username)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.User.SingleOrDefaultAsync(m => m.Username == username);

            if (user == null)
            {
                return NotFound(user);
            }


            return Ok(user.Salt);
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

    public class UsernameWrapper
    {
        public string username { get; set; }
    }
}