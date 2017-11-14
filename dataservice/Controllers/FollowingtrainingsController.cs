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
    [Route("api/Followingtrainings")]
    public class FollowingtrainingsController : Controller
    {
        private readonly _17SP2G4Context _context;

        public FollowingtrainingsController(_17SP2G4Context context)
        {
            _context = context;
        }

        // GET: api/Followingtrainings
        [HttpGet]
        public IEnumerable<Followingtraining> GetFollowingtraining()
        {
            return _context.Followingtraining;
        }

        // GET: api/Followingtrainings/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFollowingtrainingByUserId([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var followingtraining = await _context.Followingtraining.Where(m => m.UserId == id).ToListAsync();

            if (followingtraining == null)
            {
                return NotFound();
            }

            return Ok(followingtraining);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFollowingtrainingByTrainingSessionId([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var followingtraining = await _context.Followingtraining.Where(m => m.TrainingSessionId == id).ToListAsync();

            if (followingtraining == null)
            {
                return NotFound();
            }

            return Ok(followingtraining);
        }

        // PUT: api/Followingtrainings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFollowingtraining([FromRoute] int id, [FromBody] Followingtraining followingtraining)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != followingtraining.UserId)
            {
                return BadRequest();
            }

            _context.Entry(followingtraining).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FollowingtrainingExists(id))
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

        // POST: api/Followingtrainings
        [HttpPost]
        public async Task<IActionResult> PostFollowingtraining([FromBody] Followingtraining followingtraining)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Followingtraining.Add(followingtraining);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FollowingtrainingExists(followingtraining.UserId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFollowingtraining", new { id = followingtraining.UserId }, followingtraining);
        }

        // DELETE: api/Followingtrainings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFollowingtraining([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var followingtraining = await _context.Followingtraining.SingleOrDefaultAsync(m => m.UserId == id);
            if (followingtraining == null)
            {
                return NotFound();
            }

            _context.Followingtraining.Remove(followingtraining);
            await _context.SaveChangesAsync();

            return Ok(followingtraining);
        }

        private bool FollowingtrainingExists(int id)
        {
            return _context.Followingtraining.Any(e => e.UserId == id);
        }
    }
}