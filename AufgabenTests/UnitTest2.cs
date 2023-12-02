using System.Diagnostics;
using System.Drawing;
namespace AufgabenTests;

[TestClass]
public class UnitTest2
{
    public static IEnumerable<object[]> SampleLines
    {
        get
        {
            var lines = File.ReadAllLines("./../../../../Day02/sample.txt").ToArray<object>();
            var arr = new List<object[]>();
            foreach (var line in lines)
            {
                arr.Add(new object[] { line });
            }
            return arr;
        }
    }

    [DataTestMethod]
    [DynamicData(nameof(SampleLines))]
    public void Test(string line)
    {
        new GameRecord(line);
    }

    [DataTestMethod]
    [DynamicData(nameof(SampleLines))]
    public void IsPossibleTest(string line)
    {
        var bag = new List<Draw>
        {
            new Draw(12, Color.Red),
            new Draw(13, Color.Green),
            new Draw(14, Color.Blue),
        };
        var record = new GameRecord(line);
        if (record.Id == 1 | record.Id == 2 | record.Id == 5)
            Assert.IsTrue(record.IsPossible(bag));
        else
            Assert.IsFalse(record.IsPossible(bag));
    }

    [DataTestMethod]
    [DataRow(1, 4, 2, 6, 48)]
    [DataRow(2, 1, 3, 4, 12)]
    [DataRow(3, 20, 13, 6, 1560)]
    [DataRow(4, 14, 3, 15, 630)]
    [DataRow(5, 6, 3, 2, 36)]
    public void AtLeastNecessaryTest(int id, int reds, int greens, int blues, int power)
    {
        var bag = new List<Draw>
        {
            new Draw(12, Color.Red),
            new Draw(13, Color.Green),
            new Draw(14, Color.Blue),
        };
        var lines = File.ReadAllLines("./../../../../Day02/sample.txt");

        var record = new GameRecord(lines.First(l => l.StartsWith($"Game {id}:")));
        Assert.IsTrue(record.Id == id);
        var minimumBag = record.AtLeastNecessary(bag);
        Assert.IsTrue(minimumBag.First(b => b.Color == Color.Red).Amount == reds);
        Assert.IsTrue(minimumBag.First(b => b.Color == Color.Green).Amount == greens);
        Assert.IsTrue(minimumBag.First(b => b.Color == Color.Blue).Amount == blues);
        var bagPower = GameRecord.GetPower(minimumBag);
        Assert.AreEqual(power, bagPower);

    }

}
