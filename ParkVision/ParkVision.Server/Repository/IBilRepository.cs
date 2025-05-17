using ParkVision.Server.Model;

namespace ParkVision.Server.Repository
{
    public interface IBilRepository
    {
        Task<Bil?> AddAsync(Bil bil);
        Task<bool> ValidateNummerplade(string id);
        Task<bool> ExistsAsync(string id);
        Task<Bil?> DeleteAsync(string id);
        Task<IEnumerable<Bil>> GetAllAsync();
        Task<Bil?> GetByIdAsync(string id);
        Task<Bil?> UpdateAsync(string id, Bil bil);
    }
}