using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dataservice.Models;
using System;
using Microsoft.AspNetCore.Authorization;

namespace dataservice.Controllers
{
    [Authorize][AllowAnonymous]
    [Produces("application/json")]
    [Route("api/Trainingsessions")]
    public class TrainingsessionsController : Controller
    {
        private readonly _17SP2G4Context _context;

        public TrainingsessionsController(_17SP2G4Context context)
        {
            _context = context;
        }

        // GET: api/Trainingsessions
        [HttpGet]
        public IEnumerable<Trainingsession> GetTrainingsession(bool loadrelated = false)
        {
            if (loadrelated)
            {
                return _context.Trainingsession
                    .Include(t => t.Address)
                    .Include(t => t.Teacher)
                    .Include(t => t.Training)
                    //.Include(m => m.Training).ThenInclude(t => t.Trainingfaq)
                    //.Include(m => m.Training).ThenInclude(t => t.Trainingsbook)
                    //.Include(m => m.Training).ThenInclude(t => t.Trainingsession)
                    //.Include(m => m.Training).ThenInclude(t => t.Trainingsurvey)
                    .Include(t => t.Followingtraining);
            }
            return _context.Trainingsession;
        }

        // GET: api/Trainingsessions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrainingsession([FromRoute] int id, bool loadrelated = false)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Trainingsession trainingsession;

            if (loadrelated)
            {
                trainingsession = await _context.Trainingsession
                    .Include(t => t.Address)
                    .Include(t => t.Teacher)
                    .Include(t => t.Training)
                    .Include(t => t.Followingtraining)
                    .SingleOrDefaultAsync(m => m.TrainingSessionId == id);
            }

            else
            {
                trainingsession = await _context.Trainingsession.SingleOrDefaultAsync(m => m.TrainingSessionId == id);
            }
            

            if (trainingsession == null)
            {
                return NotFound();
            }

            return Ok(trainingsession);
        }

        // GET: api/Trainingsessions
        [HttpGet("future")]
        public IEnumerable<Trainingsession> GetFutureTrainingsession()
        {
            return _context.Trainingsession
                .Include(t => t.Address)
                .Where(t => !t.Cancelled && DateTime.Compare(t.Date, DateTime.Now) > 0)
                .ToList();
        }

        // GET: api/trainingsessions/5/users
        [HttpGet("{id}/users")]
        public async Task<IActionResult> GetUsers([FromRoute] int id, bool loadrelated = false)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IEnumerable<User> users;

            if (loadrelated)
            {
                users = await _context.Followingtraining
                    .Where(m => m.TrainingSessionId == id)
                    .Include(m => m.User).ThenInclude(u => u.Followingtraining)
                    .Include(m => m.User).ThenInclude(u => u.Surveyanswer)
                    .Include(m => m.User).ThenInclude(u => u.Usercertificate)
                    .Select(m => m.User)
                    .ToListAsync();
            }

            users = await _context.Followingtraining.Where(m => m.TrainingSessionId == id).Select(m => m.User).ToListAsync();

            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }

        // GET: api/trainingsessions/5/traininginfo
        [HttpGet("{id}/traininginfo")]
        public async Task<IActionResult> GetTrainingInfo([FromRoute] int id, bool loadrelated)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Traininginfo traininginfo;

            if (loadrelated)
            {
                traininginfo = await _context.Trainingsession
                    .Include(m => m.Training).ThenInclude(t => t.Certificate)
                    .Include(m => m.Training).ThenInclude(t => t.Trainingfaq)
                    .Include(m => m.Training).ThenInclude(t => t.Trainingsbook)
                    .Include(m => m.Training).ThenInclude(t => t.Trainingsession)
                    .Include(m => m.Training).ThenInclude(t => t.Trainingsurvey)
                    .Where(m => m.TrainingSessionId == id)
                    .Select(m => m.Training)
                    .FirstOrDefaultAsync();
            }

            traininginfo = await _context.Trainingsession.Include(m => m.Training).Where(m => m.TrainingSessionId == id).Select(m => m.Training).FirstOrDefaultAsync();


            if (traininginfo == null)
            {
                return NotFound();
            }

            return Ok(traininginfo);
        }

        // GET: api/trainingsessions/5/teacher
        [HttpGet("{id}/teacher")]
        public async Task<IActionResult> GetTeacher([FromRoute] int id, bool loadrelated)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Teacher teacher;
            
            if (loadrelated)
            {
                teacher = await _context.Teacher
                    .Include(t => t.Trainingsession)
                    .SingleOrDefaultAsync(m => m.TeacherId == id);
            }

            teacher = await _context.Teacher.SingleOrDefaultAsync(m => m.TeacherId == id);

            if (teacher == null)
            {
                return NotFound();
            }

            return Ok(teacher);
        }

        // GET: api/trainingsessions/5/address
        [HttpGet("{id}/address")]
        public async Task<IActionResult> GetAddress([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var address = await _context.Trainingsession.Where(m => m.TrainingSessionId == id).Select(m => m.Address).FirstOrDefaultAsync();

            if (address == null)
            {
                return NotFound();
            }

            return Ok(address);
        }

        // PUT: api/Trainingsessions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainingsession([FromRoute] int id, [FromBody] TrainingsessionUpdate u)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != u.TrainingSessionId)
            {
                return BadRequest();
            }

            Trainingsession trainingsession = new Trainingsession(u);

            _context.Entry(trainingsession).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainingsessionExists(id))
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

        // POST: api/Trainingsessions
        [HttpPost]
        public async Task<IActionResult> PostTrainingsession([FromBody] TrainingsessionUpdate trainingsession)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Trainingsession ts = new Trainingsession(trainingsession);

            _context.Trainingsession.Add(ts);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTrainingsession", new { id = trainingsession.TrainingSessionId }, trainingsession);
        }

        // DELETE: api/Trainingsessions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainingsession([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trainingsession = await _context.Trainingsession.FirstOrDefaultAsync(m => m.TrainingSessionId == id);
            if (trainingsession == null)
            {
                return NotFound();
            }

            _context.Trainingsession.Remove(trainingsession);
            await _context.SaveChangesAsync();



            return Ok(new TrainingsessionUpdate(trainingsession));
        }

        private bool TrainingsessionExists(int id)
        {
            return _context.Trainingsession.Any(e => e.TrainingSessionId == id);
        }
    }
}