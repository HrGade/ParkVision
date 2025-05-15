using ParkVision.Server.Model;

namespace ParkVision.Server.Repository;

//public class BilRepository : IBilRepository
//{
//    private readonly List<Bil> _biler = [];
//    private int _nextId = 1;

//    private bool ValidateHandelsId(int id)
//    {
//        return _biler.Where(e => e.Nummerplade == id).Any();
//    }

//    // Waiting with making XML docs.
//    public Task<Bil?> GetById(string nummerplade)
//    {
//        return Task.FromResult(_biler.Find(e => e.Nummerplade == nummerplade));
//    }

//    public async Bil Add(Bil aktieHandel)
//    {
//        if (ValidateHandelsId(aktieHandel.HandelsId))
//        {
//            throw new ArgumentException(
//                $"An Bil object already has this id: {aktieHandel.HandelsId}",
//                nameof(aktieHandel));
//        }
//        aktieHandel.HandelsId = _nextId++;
//        _biler.Add(aktieHandel);
//        return aktieHandel;
//    }

//    public Bil? Delete(int id)
//    {
//        Bil? aktieHandelToBeDeleted = GetById(id);
//        if (aktieHandelToBeDeleted == null)
//        {
//            return null;
//        }
//        _ = _biler.Remove(aktieHandelToBeDeleted);
//        return aktieHandelToBeDeleted;
//    }

//    public Bil? Update(int id, Bil data)
//    {
//        Bil? aktieHandelToBeUpdated = GetById(id);
//        if (aktieHandelToBeUpdated == null)
//        {
//            return null;
//        }
//        aktieHandelToBeUpdated.Navn = data.Navn;
//        aktieHandelToBeUpdated.Antal = data.Antal;
//        aktieHandelToBeUpdated.HandelsPris = data.HandelsPris;
//        return aktieHandelToBeUpdated;
//    }

//    public IEnumerable<Bil> Get(string? navn = null, int? antal = null,
//        HandelsPrisComparison? handelsPrisComparer = null, string? orderBy = null)
//    {
//        IEnumerable<Bil> result = GetAll();
//        if (navn != null)
//        {
//            result = result.Where(
//                e => e.Navn.Contains(navn, StringComparison.OrdinalIgnoreCase));
//        }
//        if (antal != null)
//        {
//            result = result.Where(
//                e => e.Antal == antal);
//        }
//        if (handelsPrisComparer != null)
//        {
//            result = result.Where(
//                e => handelsPrisComparer(e.HandelsPris));
//        }
//        if (orderBy != null)
//        {
//            orderBy = orderBy.ToLower();
//            switch (orderBy)
//            {
//                case "navn":
//                case "navn_asc":
//                    result = result.OrderBy(e => e.Navn);
//                    break;

//                case "navn_desc":
//                    result = result.OrderByDescending(e => e.Navn);
//                    break;

//                case "handelspris":
//                case "handelspris_asc":
//                    result = result.OrderBy(e => e.HandelsPris);
//                    break;

//                case "handelspris_desc":
//                    result = result.OrderByDescending(e => e.HandelsPris);
//                    break;
//            }
//        }
//        return result;
//    }
//}
