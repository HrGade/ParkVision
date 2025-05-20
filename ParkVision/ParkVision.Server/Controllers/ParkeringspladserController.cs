using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkVision.Server.DB;
using ParkVision.Server.Model;

namespace ParkVision.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ParkeringspladserController : ControllerBase
{
    private readonly ParkVisionDbContext _context;

    public ParkeringspladserController(ParkVisionDbContext context)
    {
        _context = context;
    }

    // GET: api/Parkeringspladser
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Parkeringsplads>>> GetParkeringspladser()
    {
        return await _context.Parkeringspladser.ToListAsync();
    }

    // GET: api/Parkeringspladser/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Parkeringsplads>> GetParkeringsplads(int id)
    {
        var parkeringsplads = await _context.Parkeringspladser.FindAsync(id);
        if (parkeringsplads == null)
        {
            return NotFound();
        }
        return Ok(parkeringsplads);
    }

    // PUT: api/Parkeringspladser/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutParkeringsplads(int id, Parkeringsplads parkeringsplads)
    {
        if (id != parkeringsplads.ParkeringspladsID)
        {
            return BadRequest();
        }
        _context.Entry(parkeringsplads).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ParkeringspladsExists(id))
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

    // POST: api/Parkeringspladser
    [HttpPost]
    public async Task<ActionResult<Parkeringsplads>> PostParkeringsplads(Parkeringsplads parkeringsplads)
    {
        _context.Parkeringspladser.Add(parkeringsplads);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetParkeringsplads", new { id = parkeringsplads.ParkeringspladsID }, parkeringsplads);
    }

    // DELETE: api/Parkeringspladser/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteParkeringsplads(int id)
    {
        var parkeringsplads = await _context.Parkeringspladser.FindAsync(id);
        if (parkeringsplads == null)
        {
            return NotFound();
        }
        _context.Parkeringspladser.Remove(parkeringsplads);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool ParkeringspladsExists(int id)
    {
        return _context.Parkeringspladser.Any(e => e.ParkeringspladsID == id);
    }
}
