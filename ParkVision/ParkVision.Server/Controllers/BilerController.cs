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
   
    private readonly BilRepositoryDB _Repository;

    public BilerController(BilRepositoryDB context)
    {
        _Repository = context;
    }

    // GET: api/Biler
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Bil>>> GetBiler()
    {
        return await _Repository.GetByIdAsync();
    }

    // GET: api/Biler/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Bil>> GetBil(string id)
    {
        var bil = await _Repository.GetBilByIdAsync(id);

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
            await _Repository.UpdateBilAsync(bil);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (await _Repository.BilExistsAsync(bil.Nummerplade))
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
            await _Repository.AddBilAsync(bil);
        }
        catch (DbUpdateException)
        {
            if (await _Repository.BilExistsAsync(bil.Nummerplade))
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
        var bil = await _Repository.GetBilByIdAsync(id);
        if (bil == null)
        {
            return NotFound();
        }

       await _Repository.DeleteBilAsync(id);
        
        return NoContent();
    }
    
}
