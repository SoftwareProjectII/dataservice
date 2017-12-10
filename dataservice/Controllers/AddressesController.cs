using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dataservice.Models;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace dataservice.Controllers
{
    [Produces("application/json")]
    [Route("api/Addresses")]
    [Authorize]
    public class AddressesController : Controller
    {
        private readonly _17SP2G4Context _context;       

        public AddressesController(_17SP2G4Context context)
        {
            _context = context;
        }

        // GET: api/Addresses
        [HttpGet]
        public IEnumerable<Address> GetAddress(bool loadrelated = false)
        {
            if (loadrelated)
            {
                return _context.Address.Include(a => a.Trainingsession);
            }
            return _context.Address;
        }

        // GET: api/Addresses/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAddress([FromRoute] int id, [FromQuery] bool loadrelated = false)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Address address;

            if (loadrelated)
            {
                address = await _context.Address
                    .Include(a => a.Trainingsession).ThenInclude(t => t.Teacher)
                    .Include(a => a.Trainingsession).ThenInclude(t => t.Training)
                    .Include(a => a.Trainingsession).ThenInclude(t => t.Followingtraining).ThenInclude(f => f.User)
                    .SingleOrDefaultAsync(m => m.AddressId == id);
            }
            else
            {
                address = await _context.Address.SingleOrDefaultAsync(m => m.AddressId == id);
            }            

            if (address == null)
            {
                return NotFound();
            }

            return Ok(address);
        }

        // GET: api/Addresses/5/trainingsessions
        [HttpGet("{id}/trainingsessions")]
        public async Task<IActionResult> GetTrainingsession([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var trainingsession = await _context.Trainingsession.Where(m => m.AddressId == id).ToListAsync();

            if (trainingsession == null)
            {
                return NotFound();
            }

            return Ok(trainingsession);
        }

        // PUT: api/Addresses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddress([FromRoute] int id, [FromBody] AddressUpdate u)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != u.AddressId)
            {
                return BadRequest();
            }

            Address address = new Address(u);

            _context.Entry(address).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(id))
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

        // POST: api/Addresses
        [HttpPost]
        public async Task<IActionResult> PostAddress([FromBody] AddressUpdate u)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Address address = new Address(u);

            _context.Address.Add(address);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAddress", new { id = address.AddressId }, address);
        }

        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddress([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var address = await _context.Address.SingleOrDefaultAsync(m => m.AddressId == id);
            if (address == null)
            {
                return NotFound();
            }

            _context.Address.Remove(address);
            await _context.SaveChangesAsync();

            return Ok(new AddressUpdate(address));
        }

        private bool AddressExists(int id)
        {
            return _context.Address.Any(e => e.AddressId == id);
        }
    }
}