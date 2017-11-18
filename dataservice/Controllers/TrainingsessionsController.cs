using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dataservice.Models;

namespace dataservice.Controllers
{
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
        public IEnumerable<Trainingsession> GetTrainingsession()
        {
            return _context.Trainingsession.Include(m => m.Training);
        }

        // GET: api/Trainingsessions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrainingsession([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trainingsession = await _context.Trainingsession.Include(m => m.Training).SingleOrDefaultAsync(m => m.TrainingSessionId == id);

            if (trainingsession == null)
            {
                return NotFound();
            }

            return Ok(trainingsession);
        }

        // GET: api/trainingsessions/5/users
        [HttpGet("{id}/users")]
        public async Task<IActionResult> GetUsers([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var users = await _context.Followingtraining.Where(m => m.TrainingSessionId == id).Select(m => m.User).ToListAsync();

            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }

        // GET: api/trainingsessions/5/traininginfo
        [HttpGet("{id}/traininginfo")]
        public async Task<IActionResult> GetTrainingInfo([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var training = await _context.Trainingsession.Include(m => m.Training).Where(m => m.TrainingSessionId == id).Select(m => m.Training).FirstOrDefaultAsync();


            if (training == null)
            {
                return NotFound();
            }

            return Ok(training);
        }

        // GET: api/trainingsessions/5/teacher
        [HttpGet("{id}/teacher")]
        public async Task<IActionResult> GetTeacher([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var teacher = await _context.Teacher.SingleOrDefaultAsync(m => m.TeacherId == id);
            
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
        public async Task<IActionResult> PutTrainingsession([FromRoute] int id, [FromBody] Trainingsession trainingsession)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != trainingsession.TrainingSessionId)
            {
                return BadRequest();
            }

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
        public async Task<IActionResult> PostTrainingsession([FromBody] Trainingsession trainingsession)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Trainingsession.Add(trainingsession);
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

            return Ok(trainingsession);
        }

        private bool TrainingsessionExists(int id)
        {
            return _context.Trainingsession.Any(e => e.TrainingSessionId == id);
        }
    }
}