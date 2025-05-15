using ParkVision.Server.Model;

namespace ParkVision.Server.Repository;

public class BilRepository : IBilRepository
{
    private readonly List<Bil> _biler = [];

    public async Task<bool> ExistsAsync(string id)
    {
        bool exists = _biler.Where(e => e.Nummerplade == id).Any();
        return await Task.FromResult(exists);
    }

    // Waiting with making XML docs.
    public async Task<Bil?> GetByIdAsync(string id)
    {
        Bil? bil = _biler.Find(e => e.Nummerplade == id);
        return await Task.FromResult(bil);
    }

    public async Task AddAsync(Bil bil)
    {
        if (await ExistsAsync(bil.Nummerplade))
        {
            throw new ArgumentException(
                $"An Bil object already has this id: {bil.Nummerplade}",
                nameof(bil));
        }
        _biler.Add(bil);
    }

    public async Task DeleteAsync(string id)
    {
        Bil? bilToBeDeleted = await GetByIdAsync(id);
        _ = _biler.Remove(bilToBeDeleted);
    }

    public async Task UpdateAsync(Bil bil)
    {
        Bil? bilToBeUpdated = await GetByIdAsync(bil.Nummerplade);
        bilToBeUpdated.Nummerplade = bil.Nummerplade;
    }

    public async Task<IEnumerable<Bil>> GetAllAsync()
    {
        IEnumerable<Bil> result = [.. _biler];
        return await Task.FromResult(result);
    }
}
