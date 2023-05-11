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
using APITrail.Connect;

namespace APITrail.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkersController : ControllerBase
    {
        private readonly TodoContextWorker _context;
 
        public WorkersController(TodoContextWorker context)
        {
            _context = context;
        }

        // GET: api/Workers
      /*  [HttpGet]
        public async Task<ActionResult<IEnumerable<Worker>>> GetTodoWorker()
        {
          if (_context.TodoWorker == null)
          {
              return NotFound();
          }
            return await _context.TodoWorker.ToListAsync();
        }
      */
        // GET: api/Workers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Worker>> GetWorker(int id)
        {

          if (_context.TodoWorker == null)
          {
              return NotFound();

          }
            Worker worker =  CWorker.GetWorkerById(id);
                //await _context.TodoWorker.FindAsync(id);


            if (worker == null)
            {
                return NotFound();
            }

            return worker;
        }

        // PUT: api/Workers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
     /*   [HttpPut("{id}")]
        public async Task<IActionResult> PutWorker(int id, Worker worker)
        {
            if (id != worker.Id)
            {
                return BadRequest();
            }

            _context.Entry(worker).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkerExists(id))
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

        // POST: api/Workers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Worker>> PostWorker(Worker worker)
        {
            CWorker.Check();
          string succeed = CWorker.AddWorker(worker);
          if (_context.TodoWorker == null)
          {
              return Problem("Entity set 'TodoContextWorker.TodoWorker'  is null.");
          }
         /* if (!succeed)
            {
               return Problem($"ID {worker.Id} already exists in the table.");
            }*/
            _context.TodoWorker.Add(worker);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetWorker", new { id = worker.Id }, worker);
            if (succeed == "ok")
                return CreatedAtAction(nameof(GetWorker), new { id = worker.Id }, worker);
            else
                return BadRequest("Failed to add Worker - " + succeed);

        }

        // DELETE: api/Workers/5
    /*    [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorker(int id)
        {
            if (_context.TodoWorker == null)
            {
                return NotFound();
            }
            var worker = await _context.TodoWorker.FindAsync(id);
            if (worker == null)
            {
                return NotFound();
            }

            _context.TodoWorker.Remove(worker);
            await _context.SaveChangesAsync();

            return NoContent();
        }*/

    /*    private bool WorkerExists(int id)
        {
            return (_context.TodoWorker?.Any(e => e.Id == id)).GetValueOrDefault();
        }*/
    }
}
