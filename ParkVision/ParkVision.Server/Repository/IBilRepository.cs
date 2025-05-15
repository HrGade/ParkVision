using ParkVision.Server.Model;

namespace ParkVision.Server.Repository
{
    public interface IBilRepository
    {
        Task AddAsync(Bil bil);
        Task<bool> ExistsAsync(string id);
        Task DeleteAsync(string id);
        Task<IEnumerable<Bil>> GetAllAsync();
        Task<Bil?> GetByIdAsync(string id);
        Task UpdateAsync(Bil bil);
    }
}