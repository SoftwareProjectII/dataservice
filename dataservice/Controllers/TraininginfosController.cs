using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dataservice.Models;

namespace dataservice.Controllers
{
    [Produces("application/json")]
    [Route("api/Traininginfos")]
    public class TraininginfosController : Controller
    {
        private readonly _17SP2G4Context _context;

        public TraininginfosController(_17SP2G4Context context)
        {
            _context = context;
        }

        // GET: api/Traininginfoes
        [HttpGet]
        public IEnumerable<Traininginfo> GetTraininginfo()
        {
            return _context.Traininginfo;
        }

        // GET: api/Traininginfoes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTraininginfo([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var traininginfo = await _context.Traininginfo.SingleOrDefaultAsync(m => m.TrainingId == id);

            if (traininginfo == null)
            {
                return NotFound();
            }

            return Ok(traininginfo);
        }

        // PUT: api/Traininginfoes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTraininginfo([FromRoute] int id, [FromBody] Traininginfo traininginfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != traininginfo.TrainingId)
            {
                return BadRequest();
            }

            _context.Entry(traininginfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TraininginfoExists(id))
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

        // POST: api/Traininginfoes
        [HttpPost]
        public async Task<IActionResult> PostTraininginfo([FromBody] Traininginfo traininginfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Traininginfo.Add(traininginfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTraininginfo", new { id = traininginfo.TrainingId }, traininginfo);
        }

        // DELETE: api/Traininginfoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTraininginfo([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var traininginfo = await _context.Traininginfo.SingleOrDefaultAsync(m => m.TrainingId == id);
            if (traininginfo == null)
            {
                return NotFound();
            }

            _context.Traininginfo.Remove(traininginfo);
            await _context.SaveChangesAsync();

            return Ok(traininginfo);
        }

        private bool TraininginfoExists(int id)
        {
            return _context.Traininginfo.Any(e => e.TrainingId == id);
        }
    }
}