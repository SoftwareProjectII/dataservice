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
    [Route("api/Companyinfoes")]
    public class CompanyinfoesController : Controller
    {
        private readonly _17SP2G4Context _context;

        public CompanyinfoesController(_17SP2G4Context context)
        {
            _context = context;
        }

        // GET: api/Companyinfoes
        [HttpGet]
        public IEnumerable<Companyinfo> GetCompanyinfo()
        {
            return _context.Companyinfo;
        }

        // GET: api/Companyinfoes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompanyinfo([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var companyinfo = await _context.Companyinfo.SingleOrDefaultAsync(m => m.CompanyId == id);

            if (companyinfo == null)
            {
                return NotFound();
            }

            return Ok(companyinfo);
        }

        // PUT: api/Companyinfoes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompanyinfo([FromRoute] int id, [FromBody] Companyinfo companyinfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != companyinfo.CompanyId)
            {
                return BadRequest();
            }

            _context.Entry(companyinfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyinfoExists(id))
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

        // POST: api/Companyinfoes
        [HttpPost]
        public async Task<IActionResult> PostCompanyinfo([FromBody] Companyinfo companyinfo)
        {
            return Forbid();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Companyinfo.Add(companyinfo);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CompanyinfoExists(companyinfo.CompanyId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCompanyinfo", new { id = companyinfo.CompanyId }, companyinfo);
        }

        // DELETE: api/Companyinfoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompanyinfo([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var companyinfo = await _context.Companyinfo.SingleOrDefaultAsync(m => m.CompanyId == id);
            if (companyinfo == null)
            {
                return NotFound();
            }

            _context.Companyinfo.Remove(companyinfo);
            await _context.SaveChangesAsync();

            return Ok(companyinfo);
        }

        private bool CompanyinfoExists(int id)
        {
            return _context.Companyinfo.Any(e => e.CompanyId == id);
        }
    }
}