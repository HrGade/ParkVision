using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkVision.Server.DB;
using ParkVision.Server.Model;

namespace ParkVision.Server.Controllers;

// Hvis det er rigtigt, så ville vi også skulle en repository til denne controller,
// men vi har vurderet, vi ikke har tid tilbage til at lave en.

[Route("api/[controller]")]
[ApiController]
public class ParkeringerController : ControllerBase
{
    private readonly ParkVisionDbContext _context;

    public ParkeringerController(ParkVisionDbContext context)
    {
        _context = context;
    }

    // GET: api/Parkeringer
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Parkering>>> GetParkering()
    {
        return await _context.Parkering.ToListAsync();
    }

    // GET: api/Parkeringer/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ParkeringDTO>> GetParkering(int id)
    {
        var parkering = await _context.Parkering.FindAsync();
        if (parkering == null)
        {
            return NotFound();
        }
        return parkering;
    }

    // PUT: api/Parkeringer/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutParkering(int id, Parkering parkering)
    {
        if (id != parkering.ParkeringID)
        {
            return BadRequest();
        }
        _context.Entry(parkering).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ParkeringExists(id))
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

    // POST: api/Parkeringer
    [HttpPost]
    public async Task<ActionResult<Parkering>> PostParkering(Parkering parkering)
    {
        _context.Parkering.Add(parkering);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetParkering", new { id = parkering.ParkeringID }, parkering);
    }

    // DELETE: api/Parkeringer/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteParkering(int id)
    {
        var parkering = await _context.Parkering.FindAsync(id);
        if (parkering == null)
        {
            return NotFound();
        }
        _context.Parkering.Remove(parkering);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool ParkeringExists(int id)
    {
        return _context.Parkering.Any(e => e.ParkeringID == id);
    }
}
