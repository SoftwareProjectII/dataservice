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
    [Authorize]
    [Produces("application/json")]
    [Route("api/Followingtrainings")]
    public class FollowingtrainingsController : Controller
    {
        private readonly _17SP2G4Context _context;

        public FollowingtrainingsController(_17SP2G4Context context)
        {
            _context = context;
        }

        // GET: api/followingtrainings
        [HttpGet]
        public async Task<IActionResult> GetFollowingTraining(int userId = 0, int trainingSessionId = 0)
        {
            if (userId == 0 && trainingSessionId == 0)
            {
                return Ok(_context.Followingtraining);
            }

            else if (userId == 0 || trainingSessionId == 0)
            {
                return BadRequest();
            }

            Followingtraining ft =  await _context.Followingtraining.Where(f => f.UserId == userId && f.TrainingSessionId == trainingSessionId)
                .FirstOrDefaultAsync();
            if (ft == null)
            {
                return NotFound();
            }

            return Ok(ft);
        }


        // PUT: api/Followingtrainings?userid=5&trainingsessionid=5
        [HttpPut]
        public async Task<IActionResult> PutFollowingtraining(int userid, int trainingSessionId, [FromBody] FollowingtrainingUpdate u)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (userid != u.UserId || trainingSessionId != u.TrainingSessionId)
            {
                return BadRequest();
            }

            Followingtraining followingtraining = new Followingtraining(u);

            _context.Entry(followingtraining).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FollowingtrainingExists(userid, trainingSessionId))
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
        public async Task<IActionResult> PostFollowingtraining([FromBody] FollowingtrainingUpdate u)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Followingtraining followingtraining = new Followingtraining(u);

            _context.Followingtraining.Add(followingtraining);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FollowingtrainingExists(followingtraining.UserId, followingtraining.TrainingSessionId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFollowingtraining", 
                new { userId = followingtraining.UserId, trainingSessionId = followingtraining.TrainingSessionId }, followingtraining);
        }

        // DELETE: api/Followingtrainings/5
        [HttpDelete]
        public async Task<IActionResult> DeleteFollowingtraining(int userId, int trainingSessionId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var followingtraining = await _context.Followingtraining
                .SingleOrDefaultAsync(m => m.UserId == userId && m.TrainingSessionId == trainingSessionId);

            if (followingtraining == null)
            {
                return NotFound();
            }

            _context.Followingtraining.Remove(followingtraining);
            await _context.SaveChangesAsync();

            return Ok(new FollowingtrainingUpdate(followingtraining));
        }

        private bool FollowingtrainingExists(int userId, int traininSessionId)
        {
            return _context.Followingtraining.Any(e => e.UserId == userId && e.TrainingSessionId == traininSessionId);
        }
    }
}