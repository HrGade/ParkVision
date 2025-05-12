using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkVision.Server.DB;
using ParkVision.Server.Model;

namespace ParkVision.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BilerController : ControllerBase
{
    private readonly BilDbContext _context;

    public BilerController(BilDbContext context)
    {
        _context = context;
    }

    // GET: api/Biler
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Bil>>> GetBiler()
    {
        return await _context.Biler.ToListAsync();
    }

    // GET: api/Biler/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Bil>> GetBil(string id)
    {
        var bil = await _context.Biler.FindAsync(id);

        if (bil == null)
        {
            return NotFound();
        }

        return bil;
    }

    // PUT: api/Biler/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBil(string id, Bil bil)
    {
        if (id != bil.Nummerplade)
        {
            return BadRequest();
        }

        _context.Entry(bil).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BilExists(id))
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

    // POST: api/Biler
    [HttpPost]
    public async Task<ActionResult<Bil>> PostBil(Bil bil)
    {
        _context.Biler.Add(bil);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            if (BilExists(bil.Nummerplade))
            {
                return Conflict();
            }
            else
            {
                throw;
            }
        }

        return CreatedAtAction("GetBil", new { id = bil.Nummerplade }, bil);
    }

    // DELETE: api/Biler/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBil(string id)
    {
        var bil = await _context.Biler.FindAsync(id);
        if (bil == null)
        {
            return NotFound();
        }

        _context.Biler.Remove(bil);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool BilExists(string id)
    {
        return _context.Biler.Any(e => e.Nummerplade == id);
    }
}
