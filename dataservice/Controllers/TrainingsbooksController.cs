using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dataservice.Models;

namespace dataservice.Controllers
{
    [Produces("application/json")]
    [Route("api/Trainingsbooks")]
    public class TrainingsbooksController : Controller
    {
        private readonly _17SP2G4Context _context;

        public TrainingsbooksController(_17SP2G4Context context)
        {
            _context = context;
        }

        // GET: api/Trainingsbooks
        [HttpGet]
        public IEnumerable<Trainingsbook> GetTrainingsbook()
        {
            return _context.Trainingsbook;
        }

        // GET: api/Trainingsbooks/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrainingsbook([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trainingsbook = await _context.Trainingsbook.SingleOrDefaultAsync(m => m.TrainingId == id);

            if (trainingsbook == null)
            {
                return NotFound();
            }

            return Ok(trainingsbook);
        }

        // PUT: api/Trainingsbooks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainingsbook([FromRoute] int id, [FromBody] Trainingsbook trainingsbook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != trainingsbook.TrainingId)
            {
                return BadRequest();
            }

            _context.Entry(trainingsbook).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainingsbookExists(id))
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

        // POST: api/Trainingsbooks
        [HttpPost]
        public async Task<IActionResult> PostTrainingsbook([FromBody] Trainingsbook trainingsbook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Trainingsbook.Add(trainingsbook);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TrainingsbookExists(trainingsbook.TrainingId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTrainingsbook", new { id = trainingsbook.TrainingId }, trainingsbook);
        }

        // DELETE: api/Trainingsbooks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainingsbook([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trainingsbook = await _context.Trainingsbook.SingleOrDefaultAsync(m => m.TrainingId == id);
            if (trainingsbook == null)
            {
                return NotFound();
            }

            _context.Trainingsbook.Remove(trainingsbook);
            await _context.SaveChangesAsync();

            return Ok(trainingsbook);
        }

        private bool TrainingsbookExists(int id)
        {
            return _context.Trainingsbook.Any(e => e.TrainingId == id);
        }
    }
}