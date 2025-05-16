using ParkVision.Server.Model;
using ParkVision.Server.Repository;

namespace ParkVisionTests;

[TestClass]
public sealed class BilRepositoryTest
{
    private readonly BilRepository _repository = new();
    private readonly Bil _validBil = new()
    {
        Nummerplade = "AF22454"
    };
    private readonly Bil _invalidBil = new()
    {
        Nummerplade = "AF224549"
    };

    [TestMethod]
    public async Task GetByIdInvalidBilIsNull()
    {
        var nullBil = await _repository.GetByIdAsync(_invalidBil.Nummerplade);
        Assert.IsNull(nullBil);
    }

    [TestMethod]
    public async Task AddValidBilToList()
    {
        Bil? addedBil = await _repository.AddAsync(_validBil);
        Bil? retreivedBil = await _repository.GetByIdAsync(_validBil.Nummerplade);
        Assert.IsNotNull(retreivedBil);
        Assert.AreEqual(addedBil, retreivedBil);
    }

    [TestMethod]
    public async Task FailAddDuplicateBilToList()
    {
        _ = await _repository.AddAsync(_validBil);
        Bil? addedBil = await _repository.AddAsync(_validBil);
        Assert.IsNull(addedBil);
    }
}
