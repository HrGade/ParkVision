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

    public async Task<Bil?> GetByIdAsync(string id)
    {
        Bil? bil = _biler.Find(e => e.Nummerplade == id);
        if (bil == null)
        {
            Console.WriteLine($"Kunne ikke finde bilen med nummerplade: {id}");
        }
        return await Task.FromResult(bil);
    }

    public async Task<Bil?> AddAsync(Bil bil)
    {
        if (bil.Nummerplade.Length > 7)
        {
            throw new ArgumentException("Nummerpladen på bilen har mere end 7 tegn:" +
                $" {bil.Nummerplade} -> {bil.Nummerplade.Length} tegn",
                nameof(bil));
        }
        if (await ExistsAsync(bil.Nummerplade))
        {
            Console.WriteLine($"En Bil har allerede dette id: {bil.Nummerplade}");
            return null;
        }
        _biler.Add(bil);
        return bil;
    }

    public async Task<Bil?> DeleteAsync(string id)
    {
        Bil? bilToBeDeleted = await GetByIdAsync(id);
        if (bilToBeDeleted == null)
        {
            return null;
        }
        _ = _biler.Remove(bilToBeDeleted);
        return bilToBeDeleted;
    }

    public async Task<Bil?> UpdateAsync(string id, Bil bil)
    {
        Bil? bilToBeUpdated = await GetByIdAsync(id);
        if (bilToBeUpdated == null)
        {
            return null;
        }
        bilToBeUpdated.Nummerplade = bil.Nummerplade;
        return bilToBeUpdated;
    }

    public async Task<IEnumerable<Bil>> GetAllAsync()
    {
        return await Task.FromResult((IEnumerable<Bil>)[.. _biler]);
    }
}
