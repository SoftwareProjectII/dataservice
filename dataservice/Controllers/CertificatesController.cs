using dataservice.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dataservice.Controllers
{
    [Produces("application/json")]
    [Route("api/Certificates")]
    public class CertificatesController : Controller
    {
        private readonly _17SP2G4Context _context;

        public CertificatesController(_17SP2G4Context context)
        {
            _context = context;
        }

        // GET: api/Certificates
        [HttpGet]
        public IEnumerable<Certificate> GetCertificate()
        {
            return _context.Certificate;
        }

        // GET: api/Certificates/5
        [HttpGet("{id}", Name = "Certificate_Single")]
        public async Task<IActionResult> GetCertificate([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var certificate = await _context.Certificate.SingleOrDefaultAsync(m => m.CertificateId == id);

            if (certificate == null)
            {
                return NotFound();
            }

            return Ok(certificate);
        }

        // GET: api/certificates/5/users
        [HttpGet("{id}/users", Name = "Certificate_Users")]
        public async Task<IActionResult> GetUsers([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var users = await _context.Usercertificate.Where(m => m.CertificateId == id).Select(m => m.User).ToListAsync();

            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }

        // GET: api/Certificates/5/training
        [HttpGet("{id}/training", Name = "Certificate_Trainings")]
        public async Task<IActionResult> GetTraining([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var training = await _context.Certificate.Where(m => m.CertificateId == id).Select(m => m.Training).FirstOrDefaultAsync();
            
            if (training == null)
            {
                return NotFound();
            }

            return Ok(training);
        }

        // PUT: api/Certificates/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCertificate([FromRoute] int id, [FromBody] Certificate certificate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != certificate.CertificateId)
            {
                return BadRequest();
            }

            _context.Entry(certificate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CertificateExists(id))
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

        // POST: api/Certificates
        [HttpPost]
        public async Task<IActionResult> PostCertificate([FromBody] Certificate certificate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Certificate.Add(certificate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCertificate", new { id = certificate.CertificateId }, certificate);
        }

        // DELETE: api/Certificates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCertificate([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var certificate = await _context.Certificate.SingleOrDefaultAsync(m => m.CertificateId == id);
            if (certificate == null)
            {
                return NotFound();
            }

            _context.Certificate.Remove(certificate);
            await _context.SaveChangesAsync();

            return Ok(certificate);
        }

        private bool CertificateExists(int id)
        {
            return _context.Certificate.Any(e => e.CertificateId == id);
        }
    }
}