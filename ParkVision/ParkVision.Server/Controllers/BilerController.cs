using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParkVision.Server.DB;
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
        return await _repository.GetAllAsync();
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

        try
        {
            await _repository.UpdateAsync(bil);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (await _repository.ExistsAsync(bil.Nummerplade))
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
        try
        {
            await _repository.AddAsync(bil);
        }
        catch (DbUpdateException)
        {
            if (await _repository.ExistsAsync(bil.Nummerplade))
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
        var bil = await _repository.GetByIdAsync(id);
        if (bil == null)
        {
            return NotFound();
        }

       await _repository.DeleteAsync(id);
        
        return NoContent();
    }
    
}
