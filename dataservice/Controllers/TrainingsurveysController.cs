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
    [Route("api/Trainingsurveys")]
    public class TrainingsurveysController : Controller
    {
        private readonly _17SP2G4Context _context;

        public TrainingsurveysController(_17SP2G4Context context)
        {
            _context = context;
        }

        // GET: api/Trainingsurveys
        [HttpGet]
        public IEnumerable<Trainingsurvey> GetTrainingsurvey()
        {
            return _context.Trainingsurvey;
        }

        // GET: api/Trainingsurveys/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrainingsurvey([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trainingsurvey = await _context.Trainingsurvey.SingleOrDefaultAsync(m => m.TrainingId == id);

            if (trainingsurvey == null)
            {
                return NotFound();
            }

            return Ok(trainingsurvey);
        }

        // PUT: api/Trainingsurveys/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainingsurvey([FromRoute] int id, [FromBody] Trainingsurvey trainingsurvey)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != trainingsurvey.TrainingId)
            {
                return BadRequest();
            }

            _context.Entry(trainingsurvey).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainingsurveyExists(id))
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

        // POST: api/Trainingsurveys
        [HttpPost]
        public async Task<IActionResult> PostTrainingsurvey([FromBody] Trainingsurvey trainingsurvey)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Trainingsurvey.Add(trainingsurvey);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TrainingsurveyExists(trainingsurvey.TrainingId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTrainingsurvey", new { id = trainingsurvey.TrainingId }, trainingsurvey);
        }

        // DELETE: api/Trainingsurveys/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainingsurvey([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trainingsurvey = await _context.Trainingsurvey.SingleOrDefaultAsync(m => m.TrainingId == id);
            if (trainingsurvey == null)
            {
                return NotFound();
            }

            _context.Trainingsurvey.Remove(trainingsurvey);
            await _context.SaveChangesAsync();

            return Ok(trainingsurvey);
        }

        private bool TrainingsurveyExists(int id)
        {
            return _context.Trainingsurvey.Any(e => e.TrainingId == id);
        }
    }
}