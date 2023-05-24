using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StorefrontAPI.Models;
using Microsoft.AspNetCore.Cors;

namespace StorefrontAPI.Controllers
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class WeaponsController : ControllerBase
    {
        private readonly storefrontContext _context;

        public WeaponsController(storefrontContext context)
        {
            _context = context;
        }

        // GET: api/Weapons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Weapon>>> GetWeapons()
        {
          if (_context.Weapons == null)
          {
              return NotFound();
          }
            var weapons = await _context.Weapons.Include("Category").Select(x => new Weapon()
            {
                WeaponId = x.WeaponId,
                Name = x.Name,
                Description = x.Description,
                CategoryId = x.CategoryId,
                ElementId = x.ElementId,
                Price = x.Price,
                ManufacturerId = x.ManufacturerId,
                Category = x.Category,
                Element = x.Element != null ? new Element()
                {
                    ElementId = x.Element.ElementId,
                    ElementType = x.Element.ElementType
                } : null,
                Manufacturer = x.Manufacturer,
                WeaponStatus = x.WeaponStatus != null ? new WeaponStatus()
                {
                    WeaponId = x.WeaponStatus.WeaponId,
                    InStock = x.WeaponStatus.InStock,
                    OutOfStock = x.WeaponStatus.OutOfStock,
                    OnOrder = x.WeaponStatus.OnOrder,
                    Discontinued = x.WeaponStatus.Discontinued
                } : null

            }).ToListAsync();

            return Ok(weapons);
        }

        // GET: api/Weapons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Weapon>> GetWeapon(int id)
        {
          if (_context.Weapons == null)
          {
              return NotFound();
          }
            var weapon = await _context.Weapons.Where(x => x.WeaponId == id).Select(x => new Weapon()
            {
                WeaponId = x.WeaponId,
                Name = x.Name,
                Description = x.Description,
                CategoryId = x.CategoryId,
                ElementId = x.ElementId,
                Price = x.Price,
                ManufacturerId = x.ManufacturerId,
                WeaponImage = x.WeaponImage,
                Category = x.Category,
                Element = x.Element != null ? new Element()
                {
                    ElementId = x.Element.ElementId,
                    ElementType = x.Element.ElementType
                } : null,
                Manufacturer = x.Manufacturer,
                WeaponStatus = x.WeaponStatus != null ? new WeaponStatus()
                {
                    WeaponId = x.WeaponStatus.WeaponId,
                    InStock = x.WeaponStatus.InStock,
                    OutOfStock = x.WeaponStatus.OutOfStock,
                    OnOrder = x.WeaponStatus.OnOrder,
                    Discontinued = x.WeaponStatus.Discontinued
                } : null

            }).FirstOrDefaultAsync();

            if (weapon == null)
            {
                return NotFound();
            }

            return weapon;
        }

        // PUT: api/Weapons/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWeapon(int id, Weapon weapon)
        {
            if (id != weapon.WeaponId)
            {
                return BadRequest();
            }

            _context.Entry(weapon).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WeaponExists(id))
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

        // POST: api/Weapons
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Weapon>> PostWeapon(Weapon weapon)
        {
          if (_context.Weapons == null)
          {
              return Problem("Entity set 'storefrontContext.Weapons'  is null.");
          }
            Weapon newWeapon = new Weapon()
            {
                Name = weapon.Name,
                Description = weapon.Description,
                CategoryId = weapon.CategoryId,
                ElementId = weapon.ElementId,
                Price = weapon.Price,
                ManufacturerId = weapon.ManufacturerId,
                WeaponImage = weapon.WeaponImage
            };

            _context.Weapons.Add(newWeapon);
            await _context.SaveChangesAsync();

            return Ok(newWeapon);
        }

        // DELETE: api/Weapons/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWeapon(int id)
        {
            if (_context.Weapons == null)
            {
                return NotFound();
            }
            var weapon = await _context.Weapons.FindAsync(id);
            if (weapon == null)
            {
                return NotFound();
            }

            _context.Weapons.Remove(weapon);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WeaponExists(int id)
        {
            return (_context.Weapons?.Any(e => e.WeaponId == id)).GetValueOrDefault();
        }
    }
}
