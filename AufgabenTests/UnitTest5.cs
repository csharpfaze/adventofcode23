using Day05;

namespace AufgabenTests;

[TestClass]
public class UnitTest5
{
    [TestMethod]
    public void Test()
    {
        var lines = File.ReadAllLines("./../../../../Day05/sample.txt");
        var mapper = new SeedMapper(lines);
    }

    [DataTestMethod]
    [DataRow(79, 81, 81, 81, 74, 78, 78, 82)]
    [DataRow(14, 14, 53, 49, 42, 42, 43, 43)]
    [DataRow(55, 57, 57, 53, 46, 82, 82, 86)]
    [DataRow(13, 13, 52, 41, 34, 34, 35, 35)]
    public void SeedSampleTest(int seed, int soil, int fertilizer, int water, int light, int temperature, int humidity, int location)
    {
        var lines = File.ReadAllLines("./../../../../Day05/sample.txt");
        var mapper = new SeedMapper(lines);
        var chain = mapper.CreateSeedChain(seed);

        Assert.AreEqual(soil, chain.ElementAt(1));
        Assert.AreEqual(fertilizer, chain.ElementAt(2));
        Assert.AreEqual(water, chain.ElementAt(3));
        Assert.AreEqual(light, chain.ElementAt(4));
        Assert.AreEqual(temperature, chain.ElementAt(5));
        Assert.AreEqual(humidity, chain.ElementAt(6));
        Assert.AreEqual(location, chain.ElementAt(7));
    }

    [TestMethod]
    public void LowestLocationSampleTest()
    {
        var lines = File.ReadAllLines("./../../../../Day05/sample.txt");
        var mapper = new SeedMapper(lines);

        Assert.AreEqual(35, mapper.Seeds.Min(seed => mapper.CreateSeedChain(seed).Last()));
    }
}
