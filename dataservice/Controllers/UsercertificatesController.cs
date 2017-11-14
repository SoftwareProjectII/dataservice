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
    [Route("api/Usercertificates")]
    public class UsercertificatesController : Controller
    {
        private readonly _17SP2G4Context _context;

        public UsercertificatesController(_17SP2G4Context context)
        {
            _context = context;
        }

        // GET: api/Usercertificates
        [HttpGet]
        public IEnumerable<Usercertificate> GetUsercertificate()
        {
            return _context.Usercertificate;
        }

        // GET: api/Usercertificates/5
        

        // PUT: api/Usercertificates/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsercertificate([FromRoute] int id, [FromBody] Usercertificate usercertificate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != usercertificate.UserId)
            {
                return BadRequest();
            }

            _context.Entry(usercertificate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsercertificateExists(id))
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

        // POST: api/Usercertificates
        [HttpPost]
        public async Task<IActionResult> PostUsercertificate([FromBody] Usercertificate usercertificate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Usercertificate.Add(usercertificate);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UsercertificateExists(usercertificate.UserId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUsercertificate", new { id = usercertificate.UserId }, usercertificate);
        }

        // DELETE: api/Usercertificates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsercertificate([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usercertificate = await _context.Usercertificate.SingleOrDefaultAsync(m => m.UserId == id);
            if (usercertificate == null)
            {
                return NotFound();
            }

            _context.Usercertificate.Remove(usercertificate);
            await _context.SaveChangesAsync();

            return Ok(usercertificate);
        }

        private bool UsercertificateExists(int id)
        {
            return _context.Usercertificate.Any(e => e.UserId == id);
        }
    }
}