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

        // GET: api/Traininginfos
        [HttpGet]
        public IEnumerable<Traininginfo> GetTraininginfo()
        {
            return _context.Traininginfo;
        }

        // GET: api/Traininginfos/5
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

        // GET: api/Traininginfos/5/faqs
        [HttpGet("{id}/faqs")]
        public async Task<IActionResult> GetFaqs([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var faqs = await _context.Trainingfaq.Where(m => m.TrainingId == id).Select(m => m.Faq).ToListAsync();

            if (faqs == null)
            {
                return NotFound();
            }

            return Ok(faqs);
        }

        // GET: api/Traininginfos/5/certificates
        [HttpGet("{id}/certificates")]
        public async Task<IActionResult> GetCertificates([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var certificates = await _context.Certificate.Where(m => m.TrainingId == id).ToListAsync();

            if (certificates == null)
            {
                return NotFound();
            }

            return Ok(certificates);
        }

        // GET: api/Traininginfos/5/sessions
        [HttpGet("{id}/sessions")]
        public async Task<IActionResult> GetSessions([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sessions = await _context.Trainingsession.Where(m => m.TrainingId == id).ToListAsync();

            if (sessions == null)
            {
                return NotFound();
            }

            return Ok(sessions);
        }

        // GET: api/Traininginfos/5/books
        [HttpGet("{id}/books")]
        public async Task<IActionResult> GetBooks([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var books = await _context.Trainingsbook.Where(m => m.TrainingId == id).Select(m => m.IsbnNavigation).ToListAsync();

            if (books == null)
            {
                return NotFound();
            }

            return Ok(books);
        }

        // GET: api/Traininginfos/5/surveys
        [HttpGet("{id}/surveys")]
        public async Task<IActionResult> GetSurveys([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var surveys = await _context.Trainingsurvey.Where(m => m.TrainingId == id).Select(m => m.Survey).ToListAsync();

            if (surveys == null)
            {
                return NotFound();
            }

            return Ok(surveys);
        }

        // PUT: api/Traininginfos/5
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

        // POST: api/Traininginfos
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

        // DELETE: api/Traininginfos/5
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