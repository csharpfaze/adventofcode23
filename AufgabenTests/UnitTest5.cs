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
}
