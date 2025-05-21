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
    public async Task<ActionResult<IEnumerable<ParkeringDTO>>> GetParkering()
    {
        List<Parkering> allParkering = await _context.Parkeringer
            // Include sørger at Bil og Parkeringsplads objekterne (deres data) bliver taget med,
            // når de bliver hentet fra databasen.
            // bliver sendt med i svaret til klienten.
            // https://learn.microsoft.com/en-us/aspnet/web-api/overview/data/using-web-api-with-entity-framework/part-4#eager-loading-versus-lazy-loading
            .Include(p => p.Bil)
            .Include(p => p.Parkeringsplads)
            .ToListAsync();
        if (allParkering.Count == 0) // Controller har ansvar for at tjekke, om der er biler.
        {
            return NoContent();
        }
        // Parkering-modellen bliver konvertet til en DTO
        // med mindre (mere relevant information),
        // som API-kalderen modtager som svar.
        List<ParkeringDTO> parkeringDTOs = [];
        foreach (var parkering in allParkering)
        {
            ParkeringDTO conversion = ConvertActor.Parkering2ParkeringDTO(parkering);
            parkeringDTOs.Add(conversion);
        }
        return Ok(parkeringDTOs);
    }

    // GET: api/Parkeringer/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ParkeringDTO>> GetParkering(int id)
    {
        Parkering? parkering = await _context.Parkeringer.FindAsync(id);
        if (parkering == null)
        {
            return NotFound();
        }
        // Model objekter der er properties i vores parkering bliver ikke automatisk hentet,
        // så vi er nødt til at load dem explicit.
        await _context.Entry(parkering).Reference(p => p.Bil).LoadAsync();
        await _context.Entry(parkering).Reference(p => p.Parkeringsplads).LoadAsync();
        ParkeringDTO conversion = ConvertActor.Parkering2ParkeringDTO(parkering);
        return Ok(conversion);
    }

    // PUT: api/Parkeringer/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutParkering(int id, ParkeringDTO parkering)
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
            throw;
        }
        return NoContent();
    }

    // POST: api/Parkeringer
    [HttpPost]
    public async Task<ActionResult<ParkeringDTO>> PostParkering(ParkeringDTOPost parkeringDTOPost)
    {
        Parkering parkering = ConvertActor.ParkeringDTOPost2Parkering(parkeringDTOPost);
        _context.Parkeringer.Add(parkering);
        Parkeringsplads? parkeringsplads = await _context.Parkeringspladser.FindAsync(parkeringDTOPost.ParkeringspladsID);
        if (parkeringsplads == null)
        {
            return BadRequest();
        }
        parkering.Parkeringsplads = parkeringsplads;
        await _context.SaveChangesAsync();
        ParkeringDTO conversion = ConvertActor.Parkering2ParkeringDTO(parkering);
        return CreatedAtAction("GetParkering", new { id = parkering.ParkeringID }, conversion);
    }

    // DELETE: api/Parkeringer/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteParkering(int id)
    {
        var parkering = await _context.Parkeringer.FindAsync(id);
        if (parkering == null)
        {
            return NotFound();
        }
        _context.Parkeringer.Remove(parkering);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    private bool ParkeringExists(int id)
    {
        return _context.Parkeringer.Any(e => e.ParkeringID == id);
    }
}
