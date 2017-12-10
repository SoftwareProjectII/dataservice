using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dataservice.Models;
using Microsoft.AspNetCore.Authorization;

namespace dataservice.Controllers
{    
    [Authorize][AllowAnonymous]
    [Produces("application/json")]
    [Route("api/Faqs")]
    public class FaqsController : Controller
    {
        private readonly _17SP2G4Context _context;

        public FaqsController(_17SP2G4Context context)
        {
            _context = context;
        }

        // GET: api/Faqs
        [HttpGet]
        public IEnumerable<Faq> GetFaq()
        {
            return _context.Faq;
        }

        // GET: api/Faqs/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFaq([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var faq = await _context.Faq.SingleOrDefaultAsync(m => m.FaqId == id);

            if (faq == null)
            {
                return NotFound();
            }

            return Ok(faq);
        }

        // GET: api/Faqs/5/trainings
        [HttpGet("{id}/trainings")]
        public async Task<IActionResult> GetTraininginfos([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trainings = await _context.Trainingfaq.Where(m => m.FaqId == id).Select(m => m.Training).ToListAsync();
          
            return Ok(trainings);
        }

        // PUT: api/Faqs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFaq([FromRoute] int id, [FromBody] Faq faq)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != faq.FaqId)
            {
                return BadRequest();
            }

            _context.Entry(faq).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FaqExists(id))
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

        // POST: api/Faqs
        [HttpPost]
        public async Task<IActionResult> PostFaq([FromBody] Faq faq)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Faq.Add(faq);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFaq", new { id = faq.FaqId }, faq);
        }

        // DELETE: api/Faqs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFaq([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var faq = await _context.Faq.SingleOrDefaultAsync(m => m.FaqId == id);
            if (faq == null)
            {
                return NotFound();
            }

            _context.Faq.Remove(faq);
            await _context.SaveChangesAsync();

            return Ok(faq);
        }

        private bool FaqExists(int id)
        {
            return _context.Faq.Any(e => e.FaqId == id);
        }
    }
}