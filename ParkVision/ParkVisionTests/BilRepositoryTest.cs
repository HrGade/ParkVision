using ParkVision.Server.Model;
using ParkVision.Server.Repository;

namespace ParkVisionTests;

[TestClass]
public sealed class BilRepositoryTest
{
    private readonly BilRepository _repository = new();
    private readonly Bil _validBil1 = new()
    {
        Nummerplade = "AF22454"
    };
    private readonly Bil _validBil2 = new()
    {
        Nummerplade = "ZC86130"
    };
    private readonly Bil _invalidBil = new()
    {
        Nummerplade = "AF224549"
    };
    private static string[] _validTestNummerplader;

    [ClassInitialize]
    public static void InitializeTestData(TestContext testContext)
    {
        _validTestNummerplader =
            [
            "AF22454",
            "ZC86130",
            "XV16585",
            "BW73257",
            "YK61933"
            ];
    }

    [TestMethod]
    public async Task DoesNotExistsBil()
    {
        bool exists = await _repository.ExistsAsync(_validBil1.Nummerplade);
        Assert.IsFalse(exists);
    }

    [TestMethod]
    public async Task ExistsBil()
    {
        _ = await _repository.AddAsync(_validBil1);
        bool exists = await _repository.ExistsAsync(_validBil1.Nummerplade);
        Assert.IsTrue(exists);
    }

    [TestMethod]
    public async Task GetByIdInvalidNummerplade()
    {
        var nullBil = await _repository.GetByIdAsync(_invalidBil.Nummerplade);
        Assert.IsNull(nullBil);
    }

    [TestMethod]
    public async Task AddValidBilToList()
    {
        Bil? addedBil = await _repository.AddAsync(_validBil1);
        Assert.IsNotNull(addedBil);
        Bil? retreivedBil = await _repository.GetByIdAsync(_validBil1.Nummerplade);
        Assert.IsNotNull(retreivedBil);
    }

    [TestMethod]
    public async Task FailAddInvalidBilToList()
    {
        Bil? retreivedBil = await _repository.GetByIdAsync(_invalidBil.Nummerplade);
        Assert.IsNull(retreivedBil);
    }

    [TestMethod]
    public async Task FailAddDuplicateBilToList()
    {
        _ = await _repository.AddAsync(_validBil1);
        Bil? addedBil = await _repository.AddAsync(_validBil1);
        Assert.IsNull(addedBil);
    }

    [TestMethod]
    public async Task UpdateBilInList()
    {
        Bil oldBil = _validBil1;
        _ = await _repository.AddAsync(oldBil);
        Bil? changedBil = await _repository.UpdateAsync(oldBil.Nummerplade, _validBil2);
        Assert.IsNotNull(changedBil);
        Assert.AreEqual(oldBil, changedBil);
    }

    [TestMethod]
    public async Task UpdateBilInListWithInvalidNummerplade()
    {
        Bil oldBil = _validBil1;
        _ = await _repository.AddAsync(oldBil);
        Bil? changedBil = await _repository.UpdateAsync(oldBil.Nummerplade, _invalidBil);
        Assert.IsNull(changedBil);
    }

    [TestMethod]
    public async Task DeleteBilInList()
    {
        Bil? bil = await _repository.AddAsync(_validBil1);
        Bil? deletedBil = await _repository.DeleteAsync(bil.Nummerplade);
        Assert.IsNotNull(deletedBil);
        bool deletedBilExists = await _repository.ExistsAsync(deletedBil.Nummerplade);
        Assert.IsFalse(deletedBilExists);
    }

    [TestMethod]
    public async Task GetAllBilerInList()
    {
        foreach (var nummerplade in _validTestNummerplader)
        {
            _ = await _repository.AddAsync(new Bil { Nummerplade = nummerplade });
        }
        IEnumerable<Bil> biler = await _repository.GetAllAsync();
        bool hasSameBilAmount = biler.Count() == _validTestNummerplader.Length;
        Assert.IsTrue(hasSameBilAmount);
    }

    [TestMethod]
    public async Task ValidateInvalidNummerplade()
    {
        bool isValidNummerplade = await _repository.ValidateNummerplade(_validBil1.Nummerplade);
        Assert.IsTrue(isValidNummerplade);
    }

    [TestMethod]
    public async Task ValidateValidNummerplade()
    {
        bool isValidNummerplade = await _repository.ValidateNummerplade(_invalidBil.Nummerplade);
        Assert.IsFalse(isValidNummerplade);
    }
}
