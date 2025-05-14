using Microsoft.EntityFrameworkCore;
using ParkVision.Server.DB;
using ParkVision.Server.Model;

namespace ParkVision.Server.Repository
{
    public class BilRepositoryDB : IBilRepository
    {
        private readonly BilDbContext _context;

        public BilRepositoryDB(BilDbContext context)
        {
            _context = context;
        }

        public async Task<List<Bil>> GetByIdAsync()
        {
            return await _context.Biler.ToListAsync();
        }

        public async Task<Bil> GetBilByIdAsync(string id)
        {
            return await _context.Biler.FindAsync(id);
        }

        public async Task AddBilAsync(Bil bil)
        {
            await _context.Biler.AddAsync(bil);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateBilAsync(Bil bil)
        {
            _context.Biler.Update(bil);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBilAsync(string id)
        {
            var bil = await GetBilByIdAsync(id);
            if (bil != null)
            {
                _context.Biler.Remove(bil);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> BilExistsAsync(string id)
        {
            return await _context.Biler.AnyAsync(e => e.Nummerplade == id);
        }

    }


}
