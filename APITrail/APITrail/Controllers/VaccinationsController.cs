using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APITrail.Conect;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using TodoApi.Models;

namespace APITrail.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VaccinationsController : ControllerBase
    {
        private readonly TodoContextVaccination _context;

        public VaccinationsController(TodoContextVaccination context)
        {
            _context = context;
        }

        // GET: api/Vaccinations
        // [HttpGet]
        /* public async Task<ActionResult<IEnumerable<Vaccination>>> GetTodoVaccination()
         {
           if (_context.TodoVaccination == null)
           {
               return NotFound();
           }
             return await _context.TodoVaccination.ToListAsync();
         }
        */
        // GET: api/Vaccinations/5
        [HttpPost("api/Vaccination/DiffertialSync")]
       /* public async Task<IActionResult> PostVaccinations(int id)
        {
            if (_context.TodoVaccination == null)
            {
                return NotFound();
            }
            List<Vaccination> vaccination = CVaccination.GetVaccinationById(id);
            //await _context.TodoVaccination.FindAsync(id);

            if (vaccination == null)
            {
                return NotFound();
            }
            try
            {
                foreach ( var vacc in vaccination)
                {
                    _context.Entry(vacc).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                return Ok(true);
            }
            catch(Exception ex)
            {
                return Ok(false);
            }
           


        }*/
        [HttpGet("{id}")]
        public async Task<ActionResult<Vaccination>> GetVaccination(int id)
        {
          Vaccination vaccinations;
          if (_context.TodoVaccination == null)
          {
              return NotFound();
          }
            var vaccination = CVaccination.GetVaccinationById(id);
                //await _context.TodoVaccination.FindAsync(id);

            if (vaccination == null)
            {
                return NotFound();
            }
            
            return vaccination;
        }

        // PUT: api/Vaccinations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
     /*   [HttpPut("{id}")]
        public async Task<IActionResult> PutVaccination(int id, Vaccination vaccination)
        {
            if (id != vaccination.Id)
            {
                return BadRequest();
            }

            _context.Entry(vaccination).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VaccinationExists(id))
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
     */

        // POST: api/Vaccinations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Vaccination>> PostVaccination(Vaccination vaccination)
        {
            string succeed = CVaccination.AddVaccination(vaccination);
          if (_context.TodoVaccination == null)
          {
              return Problem("Entity set 'TodoContextVaccination.TodoVaccination'  is null.");
          }
            _context.TodoVaccination.Add(vaccination);
            await _context.SaveChangesAsync();
            if (succeed == "ok")
                return CreatedAtAction(nameof(GetVaccination), new { id = vaccination.Id }, vaccination);
            else
                return BadRequest("Failed to add Vaccination - " + succeed);
        }

        // DELETE: api/Vaccinations/5
    /*    [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVaccination(int id)
        {
            if (_context.TodoVaccination == null)
            {
                return NotFound();
            }
            var vaccination = await _context.TodoVaccination.FindAsync(id);
            if (vaccination == null)
            {
                return NotFound();
            }

            _context.TodoVaccination.Remove(vaccination);
            await _context.SaveChangesAsync();

            return NoContent();
        }*/

    /*    private bool VaccinationExists(int id)
        {
            return (_context.TodoVaccination?.Any(e => e.Id == id)).GetValueOrDefault();
        }*/
    }
}
