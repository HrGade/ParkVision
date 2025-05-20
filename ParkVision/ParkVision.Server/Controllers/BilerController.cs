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
    public async Task<ActionResult<IEnumerable<BilDTO>>> GetBiler()
    {
        var biler = await _repository.GetAllAsync();
        if (!biler.Any()) // Controller har ansvar for at tjekke, om der er biler.
        {
            return NoContent();
        }
        List<BilDTO> bilerDTO = [];
        foreach (var bil in biler)
        {
            BilDTO conversion = ConvertActor.Bil2BilDTO(bil);
            bilerDTO.Add(conversion);
        }
        return Ok(bilerDTO);
    }

    // GET: api/Biler/5
    [HttpGet("{id}")]
    public async Task<ActionResult<BilDTO>> GetBil(string id)
    {
        var bil = await _repository.GetByIdAsync(id);
        if (bil == null)
        {
            return NotFound();
        }
        BilDTO conversion = ConvertActor.Bil2BilDTO(bil);
        return Ok(conversion);
    }

    // PUT: api/Biler/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<ActionResult<BilDTO>> PutBil(string id, BilDTO bil)
    {
        if (id == bil.Bil.Nummerplade)
        {
            return BadRequest();
        }
        if (!await _repository.ValidateNummerplade(bil.Bil.Nummerplade))
        {
            return BadRequest();
        }
        Bil? changedBil = await _repository.UpdateAsync(id, bil.Bil);
        if (changedBil == null)
        {
            return NotFound();
        }
        BilDTO conversion = ConvertActor.Bil2BilDTO(changedBil);
        return Ok(conversion);
    }

    // POST: api/Biler
    [HttpPost]
    public async Task<ActionResult<BilDTO>> PostBil(BilDTO bil)
    {
        if (!await _repository.ValidateNummerplade(bil.Bil.Nummerplade))
        {
            return BadRequest();
        }
        if (await _repository.AddAsync(bil.Bil) == null)
        {
            return Conflict();
        }
        return CreatedAtAction(nameof(GetBil), new { id = bil.Bil.Nummerplade }, ConvertActor.Bil2BilDTO(bil.Bil));
    }

    // DELETE: api/Biler/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBil(string id)
    {
        Bil? bil = await _repository.DeleteAsync(id);
        if (bil == null)
        {
            return NotFound();
        }
        return NoContent();
    }
}
