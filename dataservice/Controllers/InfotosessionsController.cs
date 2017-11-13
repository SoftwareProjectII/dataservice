using System;
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
    [Route("api/Infotosessions")]
    public class InfotosessionsController : Controller
    {
        private readonly _17SP2G4Context _context;

        public InfotosessionsController(_17SP2G4Context context)
        {
            _context = context;
        }

        // GET: api/Infotosessions
        [HttpGet]
        public IEnumerable<Infotosession> GetInfotosession()
        {
            return _context.Infotosession;
        }

        // GET: api/Infotosessions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetInfotosession([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var infotosession = await _context.Infotosession.SingleOrDefaultAsync(m => m.TrainingId == id);

            if (infotosession == null)
            {
                return NotFound();
            }

            return Ok(infotosession);
        }

        // PUT: api/Infotosessions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInfotosession([FromRoute] int id, [FromBody] Infotosession infotosession)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != infotosession.TrainingId)
            {
                return BadRequest();
            }

            _context.Entry(infotosession).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InfotosessionExists(id))
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

        // POST: api/Infotosessions
        [HttpPost]
        public async Task<IActionResult> PostInfotosession([FromBody] Infotosession infotosession)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Infotosession.Add(infotosession);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (InfotosessionExists(infotosession.TrainingId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetInfotosession", new { id = infotosession.TrainingId }, infotosession);
        }

        // DELETE: api/Infotosessions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInfotosession([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var infotosession = await _context.Infotosession.SingleOrDefaultAsync(m => m.TrainingId == id);
            if (infotosession == null)
            {
                return NotFound();
            }

            _context.Infotosession.Remove(infotosession);
            await _context.SaveChangesAsync();

            return Ok(infotosession);
        }

        private bool InfotosessionExists(int id)
        {
            return _context.Infotosession.Any(e => e.TrainingId == id);
        }
    }
}