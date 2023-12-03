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


    [TestMethod]
    public void ValidateGearsSampleTest()
    {
        var gears = engineValidator?.GetGears();
        Assert.IsNotNull(gears);
        var gearRatio1 = gears.First().GetRatio();
        Assert.AreEqual(gearRatio1, 16345);
        var gearRatio2 = gears.ElementAt(1).GetRatio();
        Assert.AreEqual(gearRatio2, 451490);

        Assert.AreEqual(gearRatio1 + gearRatio2, 467835);
    }
}
