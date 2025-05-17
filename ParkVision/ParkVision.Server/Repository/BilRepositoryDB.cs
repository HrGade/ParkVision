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

    public async Task<Bil?> AddAsync(Bil bil)
    {
        if (!await ValidateNummerplade(bil.Nummerplade))
        {
            return null;
        }
        _ = await _context.Biler.AddAsync(bil);
        try
        {
            _ = await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            if (await ExistsAsync(bil.Nummerplade))
            {
                return null;
            }
            throw;
        }
        return bil;
    }

    public async Task<Bil?> UpdateAsync(string id, Bil bil)
    {
        if (!await ValidateNummerplade(bil.Nummerplade))
        {
            return null;
        }
        _ = _context.Biler.Update(bil);
        try
        {
            _ = await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
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
        Bil? bil = await GetByIdAsync(id);
        if (bil == null)
        {
            return null;
        }
        _ = _context.Biler.Remove(bil);
        _ = await _context.SaveChangesAsync();
        return bil;
    }

    public async Task<bool> ExistsAsync(string id)
    {
        return await _context.Biler.AnyAsync(e => e.Nummerplade == id);
    }

    public async Task<bool> ValidateNummerplade(string id)
    {
        return await Task.Run(() =>
        {
            return id.Length <= 7;
        });
    }
}
