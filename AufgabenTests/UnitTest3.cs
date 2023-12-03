using Day03;
namespace AufgabenTests;

[TestClass]
public class UnitTest3
{
    private EngineValidator? engineValidator;
    [TestInitialize]
    public void GetMap()
    {
        var map = File.ReadAllLines("./../../../../Day03/sample.txt");
        engineValidator = new EngineValidator(map);
    }

    [TestMethod]
    public void ValidateEnginePartsSampleTest()
    {
        var parts = engineValidator?.GetEngineParts();
        Assert.AreEqual(parts?.Sum(), 4361);
    }


    // [TestMethod]
    // public void ValidateGearsSampleTest()
    // {
    //     var parts = engineValidator?.GetGears();
    //     Assert.AreEqual(parts?.Pro(), 16345);
    // }
}
