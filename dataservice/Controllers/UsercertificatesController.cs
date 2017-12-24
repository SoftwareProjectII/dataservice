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

        // PUT: api/Usercertificates/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsercertificate(int userid, int trainingid, [FromBody] Usercertificate usercertificate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (userid != usercertificate.UserId || trainingid != usercertificate.TrainingId)
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
                if (!UsercertificateExists(userid, usercertificate.CertificateId))
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
                if (UsercertificateExists(usercertificate.UserId, usercertificate.CertificateId))
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

        // DELETE: api/Usercertificates?trainingid=5&userid=5
        [HttpDelete()]
        public async Task<IActionResult> DeleteUsercertificate(int trainingid, int userid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usercertificate = await _context.Usercertificate.SingleOrDefaultAsync(m => m.UserId == userid && m.TrainingId == trainingid);
            if (usercertificate == null)
            {
                return NotFound();
            }

            _context.Usercertificate.Remove(usercertificate);
            await _context.SaveChangesAsync();

            return Ok(usercertificate);
        }

        private bool UsercertificateExists(int userid, int certificateid)
        {
            return _context.Usercertificate.Any(e => e.UserId == userid && e.CertificateId == certificateid);
        }
    }
}