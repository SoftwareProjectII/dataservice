using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dataservice.Models;
using Microsoft.AspNetCore.Authorization;

namespace dataservice.Controllers
{
    [Authorize][AllowAnonymous]
    [Produces("application/json")]
    [Route("api/Trainingfaqs")]
    public class TrainingfaqsController : Controller
    {
        private readonly _17SP2G4Context _context;

        public TrainingfaqsController(_17SP2G4Context context)
        {
            _context = context;
        }

        // GET: api/Trainingfaqs
        [HttpGet]
        public IEnumerable<Trainingfaq> GetTrainingfaq()
        {
            return _context.Trainingfaq;
        }

        // PUT: api/Trainingfaqs/5

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainingfaq([FromRoute] int id, [FromBody] Trainingfaq trainingfaq)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != trainingfaq.TrainingId)
            {
                return BadRequest();
            }

            _context.Entry(trainingfaq).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainingfaqExists(id))
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

        // POST: api/Trainingfaqs
        [HttpPost]
        public async Task<IActionResult> PostTrainingfaq([FromBody] Trainingfaq trainingfaq)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Trainingfaq.Add(trainingfaq);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TrainingfaqExists(trainingfaq.TrainingId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTrainingfaq", new { id = trainingfaq.TrainingId }, trainingfaq);
        }

        // DELETE: api/Trainingfaqs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainingfaq([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trainingfaq = await _context.Trainingfaq.SingleOrDefaultAsync(m => m.TrainingId == id);
            if (trainingfaq == null)
            {
                return NotFound();
            }

            _context.Trainingfaq.Remove(trainingfaq);
            await _context.SaveChangesAsync();

            return Ok(trainingfaq);
        }

        private bool TrainingfaqExists(int id)
        {
            return _context.Trainingfaq.Any(e => e.TrainingId == id);
        }
    }
}