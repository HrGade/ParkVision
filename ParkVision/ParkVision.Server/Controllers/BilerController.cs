using Microsoft.AspNetCore.Mvc;
using ParkVision.Server.Model;
using ParkVision.Server.Repository;

namespace ParkVision.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BilerController : ControllerBase
{
   
    private readonly BilRepositoryDB _repository;

    public BilerController(BilRepositoryDB context)
    {
        _repository = context;
    }

    // GET: api/Biler
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Bil>>> GetBiler()
    {
        var biler = await _repository.GetAllAsync();
        if (!biler.Any())
        {
            return NoContent();
        }
        return Ok(biler);
    }

    // GET: api/Biler/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Bil>> GetBil(string id)
    {
        var bil = await _repository.GetByIdAsync(id);
        if (bil == null)
        {
            return NotFound();
        }
        return Ok(bil);
    }

    // PUT: api/Biler/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBil(string id, Bil bil)
    {
        if (id == bil.Nummerplade)
        {
            return BadRequest();
        }
        Bil? changedBil = await _repository.UpdateAsync(id, bil);
        if (changedBil == null)
        {
            return NotFound();
        }
        return Ok(changedBil);
    }

    // POST: api/Biler
    [HttpPost]
    public async Task<ActionResult<Bil>> PostBil(Bil bil)
    {
        if (await _repository.AddAsync(bil) == null)
        {
            return Conflict();
        }
        return CreatedAtAction("GetBil", new { id = bil.Nummerplade }, bil);
    }

    // DELETE: api/Biler/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBil(string id)
    {
        Bil? bil = await _repository.GetByIdAsync(id);
        if (bil == null)
        {
            return NotFound();
        }
        _ = await _repository.DeleteAsync(id);
        return Ok(bil);
    }
}
