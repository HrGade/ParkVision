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
    public void GetByIdIsNullTest()
    {
        var nullAktieHandel = _repository.GetByIdAsync("");
        Assert.IsNull(nullAktieHandel);
    }

    [TestMethod]
    public async void AddValidAktieHandelToListTest()
    {
        Bil addedAktieHandel = await _repository.AddAsync(_validBil);
        Bil? retreivedAktieHanel = await _repository.GetByIdAsync("AF22454");
        Assert.IsNotNull(retreivedAktieHanel);
        Assert.AreEqual(addedAktieHandel, retreivedAktieHanel);
    }

    [TestMethod]
    public void FailAddDuplicateHandelsIdToListTest()
    {
        _ = await _repository.AddAsync(_validBil);
        Assert.ThrowsException<ArgumentException>(async () => await _repository.AddAsync(_invalidBil));
    }
}
