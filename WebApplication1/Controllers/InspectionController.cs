using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InspectionController : ControllerBase
    {
        private readonly DataContext _context;

        public InspectionController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Inspection
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inspection>>> GetInspections()
        {
            if (_context.Inspections == null)
            {
                return NotFound();
            }

            return await _context.Inspections.ToListAsync();
        }

        // GET: api/Inspection/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Inspection>> GetInspection(int id)
        {
            if (_context.Inspections == null)
            {
                return NotFound();
            }

            var inspection = await _context.Inspections.FindAsync(id);

            if (inspection == null)
            {
                return NotFound();
            }

            return inspection;
        }

        // PUT: api/Inspection/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInspection(int id, Inspection inspection)
        {
            if (id != inspection.Id)
            {
                return BadRequest();
            }

            _context.Entry(inspection).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InspectionExists(id))
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

        // POST: api/Inspection
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Inspection>> PostInspection(Inspection inspection)
        {
            if (_context.Inspections == null)
            {
                return Problem("Entity set 'DataContext.Inspections'  is null.");
            }

            _context.Inspections.Add(inspection);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInspection", new { id = inspection.Id }, inspection);
        }

        // DELETE: api/Inspection/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInspection(int id)
        {
            if (_context.Inspections == null)
            {
                return NotFound();
            }

            var inspection = await _context.Inspections.FindAsync(id);
            if (inspection == null)
            {
                return NotFound();
            }

            _context.Inspections.Remove(inspection);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InspectionExists(int id)
        {
            return (_context.Inspections?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}