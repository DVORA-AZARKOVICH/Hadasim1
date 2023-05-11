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
    public class KoronasController : ControllerBase
    {
        private readonly TodoContextKorona _context;

        public KoronasController(TodoContextKorona context)
        {
            _context = context;
        }

        // GET: api/Koronas
      //  [HttpGet]
    /*    public async Task<ActionResult<IEnumerable<Korona>>> GetTodoKorona()
        {
          if (_context.TodoKorona == null)
          {
              return NotFound();
          }
            return await _context.TodoKorona.ToListAsync();
        }*/

        // GET: api/Koronas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Korona>> GetKorona(int id)
        {

          if (_context.TodoKorona == null)
          {
              return NotFound();
          }
            var korona = CKorona.GetKoronaById(id);
                //await _context.TodoKorona.FindAsync(id);

            if (korona == null)
            {
                return NotFound();
            }

            return korona;
        }

        // PUT: api/Koronas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /*    [HttpPut("{id}")]
        public async Task<IActionResult> PutKorona(int id, Korona korona)
        {
            if (id != korona.Id)
            {
                return BadRequest();
            }

            _context.Entry(korona).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KoronaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }*/

        // POST: api/Koronas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Korona>> PostKorona(Korona korona)
        {
            string  succeed = CKorona.AddKorona(korona);
           
          if (_context.TodoKorona == null)
          {
              return Problem("Entity set 'TodoContextKorona.TodoKorona'  is null.");
          }
            _context.TodoKorona.Add(korona);
            await _context.SaveChangesAsync();

            if (succeed =="ok")
                return CreatedAtAction(nameof(GetKorona), new { id = korona.Id }, korona);
            else
                return BadRequest("Failed to add Korona - "+succeed);
            int x = 5;

        }


        // DELETE: api/Koronas/5
        /*[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKorona(int id)
        {
            if (_context.TodoKorona == null)
            {
                return NotFound();
            }
            var korona = await _context.TodoKorona.FindAsync(id);

            if (korona == null)
            {
                return NotFound();
            }

            _context.TodoKorona.Remove(korona);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        */
       /* private bool KoronaExists(int id)
        {
            return (_context.TodoKorona?.Any(e => e.Id == id)).GetValueOrDefault();
        }*/
    }
}
