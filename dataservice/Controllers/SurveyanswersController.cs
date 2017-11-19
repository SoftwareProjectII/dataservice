using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dataservice.Models;

namespace dataservice.Controllers
{
    [Produces("application/json")]
    [Route("api/Surveyanswers")]
    public class SurveyanswersController : Controller
    {
        private readonly _17SP2G4Context _context;

        public SurveyanswersController(_17SP2G4Context context)
        {
            _context = context;
        }

        // GET: api/Surveyanswers
        [HttpGet]
        public IEnumerable<Surveyanswer> GetSurveyanswer()
        {
            return _context.Surveyanswer;
        }

        // GET: api/Surveyanswers/5
        [HttpGet("{id}", Name = "Surveyanswer_Single")]
        public async Task<IActionResult> GetSurveyanswer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var surveyanswer = await _context.Surveyanswer.SingleOrDefaultAsync(m => m.AnswerId == id);

            if (surveyanswer == null)
            {
                return NotFound();
            }

            return Ok(surveyanswer);
        }

        // GET: api/Surveyanswers/5/question
        [HttpGet("{id}/question", Name = "Surveyanswer_Question")]
        public async Task<IActionResult> GetQuestion([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var question = await _context.Surveyanswer.Where(m => m.AnswerId == id).Select(m => m.Question).FirstOrDefaultAsync();

            if (question == null)
            {
                return NotFound();
            }

            return Ok(question);
        }

        // GET: api/Surveyanswers/5/user
        [HttpGet("{id}/user", Name = "Surveyanswer_user")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.Surveyanswer.Where(m => m.AnswerId == id).Select(m => m.User).FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Surveyanswers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSurveyanswer([FromRoute] int id, [FromBody] Surveyanswer surveyanswer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != surveyanswer.AnswerId)
            {
                return BadRequest();
            }

            _context.Entry(surveyanswer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SurveyanswerExists(id))
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

        // POST: api/Surveyanswers
        [HttpPost]
        public async Task<IActionResult> PostSurveyanswer([FromBody] Surveyanswer surveyanswer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Surveyanswer.Add(surveyanswer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSurveyanswer", new { id = surveyanswer.AnswerId }, surveyanswer);
        }

        // DELETE: api/Surveyanswers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSurveyanswer([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var surveyanswer = await _context.Surveyanswer.SingleOrDefaultAsync(m => m.AnswerId == id);
            if (surveyanswer == null)
            {
                return NotFound();
            }

            _context.Surveyanswer.Remove(surveyanswer);
            await _context.SaveChangesAsync();

            return Ok(surveyanswer);
        }

        private bool SurveyanswerExists(int id)
        {
            return _context.Surveyanswer.Any(e => e.AnswerId == id);
        }
    }
}