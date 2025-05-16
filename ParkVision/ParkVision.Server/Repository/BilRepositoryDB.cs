using Microsoft.EntityFrameworkCore;
using ParkVision.Server.DB;
using ParkVision.Server.Model;

namespace ParkVision.Server.Repository;

public class BilRepositoryDB : IBilRepository
{
    private readonly BilDbContext _context;

    public BilRepositoryDB(BilDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Bil>> GetAllAsync()
    {
        return await _context.Biler.ToListAsync();
    }

    public async Task<Bil?> GetByIdAsync(string id)
    {
        return await _context.Biler.FindAsync(id);
    }

    public async Task<Bil> AddAsync(Bil bil)
    {
        await _context.Biler.AddAsync(bil);
        await _context.SaveChangesAsync();
        return bil;
    }

    public async Task<Bil?> UpdateAsync(string id, Bil bil)
    {
        try
    {
        _context.Biler.Update(bil);
        await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await ExistsAsync(bil.Nummerplade))
            {
                return null;
            }
            throw;
        }
        return bil;
    }

    public async Task<Bil?> DeleteAsync(string id)
    {
        var bil = await GetByIdAsync(id);
        if (bil != null)
        {
            _context.Biler.Remove(bil);
            await _context.SaveChangesAsync();
            return bil;
        }
        return null;
    }

    public async Task<bool> ExistsAsync(string id)
    {
        return await _context.Biler.AnyAsync(e => e.Nummerplade == id);
    }

}
