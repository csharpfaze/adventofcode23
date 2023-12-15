using Day06;

namespace AufgabenTests;

[TestClass]
public class UnitTest6
{
    [DataTestMethod]
    [DataRow(0, 0)]
    [DataRow(1, 6)]
    [DataRow(2, 10)]
    [DataRow(3, 12)]
    [DataRow(4, 12)]
    [DataRow(5, 10)]
    [DataRow(6, 6)]
    [DataRow(7, 0)]
    public void RangeSampleTest(int holdTime, int expectedRange)
    {
        var race = new Race(File.ReadAllLines("./../../../../Day06/sample.txt"));
        Assert.AreEqual(expectedRange, SpeedCalculation.Range(race.Rounds.First().Time, holdTime));
    }

    [TestMethod]
    public void MarginSampleTest()
    {
        var race = new Race(File.ReadAllLines("./../../../../Day06/sample.txt"));
        Assert.AreEqual(288, SpeedCalculation.Margin(SpeedCalculation.GetRacePossibilites(race)));
    }

    [TestMethod]
    public void Race2ParsingTest()
    {
        var race = new Race2(File.ReadAllLines("./../../../../Day06/sample.txt"));
        Assert.AreEqual(71530, race.Round.Time);
        Assert.AreEqual(940200, race.Round.Distance);
    }

    [TestMethod]
    public void Race2Possibilites()
    {
        var race = new Race2(File.ReadAllLines("./../../../../Day06/sample.txt"));
        Assert.AreEqual(71503, SpeedCalculation.GetRoundPossibilty(race.Round));
    }

}
