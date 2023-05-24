using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StorefrontAPI.Models;

namespace StorefrontAPI.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class WeaponStatusController : ControllerBase
    {
        private readonly storefrontContext _context;

        public WeaponStatusController(storefrontContext context)
        {
            _context = context;
        }

        // GET: api/WeaponStatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeaponStatus>>> GetWeaponStatuses()
        {
          if (_context.WeaponStatuses == null)
          {
              return NotFound();
          }
            return await _context.WeaponStatuses.ToListAsync();
        }

        // GET: api/WeaponStatus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WeaponStatus>> GetWeaponStatus(int id)
        {
          if (_context.WeaponStatuses == null)
          {
              return NotFound();
          }
            var weaponStatus = await _context.WeaponStatuses.FindAsync(id);

            if (weaponStatus == null)
            {
                return NotFound();
            }

            return weaponStatus;
        }

        // PUT: api/WeaponStatus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWeaponStatus(int id, WeaponStatus weaponStatus)
        {
            if (id != weaponStatus.WeaponId)
            {
                return BadRequest();
            }

            _context.Entry(weaponStatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WeaponStatusExists(id))
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

        // POST: api/WeaponStatus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<WeaponStatus>> PostWeaponStatus(WeaponStatus weaponStatus)
        {
          if (_context.WeaponStatuses == null)
          {
              return Problem("Entity set 'storefrontContext.WeaponStatuses'  is null.");
          }
            _context.WeaponStatuses.Add(weaponStatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWeaponStatus", new { id = weaponStatus.WeaponId }, weaponStatus);
        }

        // DELETE: api/WeaponStatus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWeaponStatus(int id)
        {
            if (_context.WeaponStatuses == null)
            {
                return NotFound();
            }
            var weaponStatus = await _context.WeaponStatuses.FindAsync(id);
            if (weaponStatus == null)
            {
                return NotFound();
            }

            _context.WeaponStatuses.Remove(weaponStatus);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WeaponStatusExists(int id)
        {
            return (_context.WeaponStatuses?.Any(e => e.WeaponId == id)).GetValueOrDefault();
        }
    }
}
