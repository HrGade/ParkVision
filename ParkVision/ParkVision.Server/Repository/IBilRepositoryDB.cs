using ParkVision.Server.Model;

namespace ParkVision.Server.Repository
{
    public interface IBilRepositoryDB
    {
        Task AddBilAsync(Bil bil);
        Task<bool> BilExistsAsync(string id);
        Task DeleteBilAsync(string id);
        Task<List<Bil>> GetByIdAsync();
        Task<Bil> GetBilByIdAsync(string id);
        Task UpdateBilAsync(Bil bil);
    }
}