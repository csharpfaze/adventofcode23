using Day04;

namespace AufgabenTests;

[TestClass]
public class UnitTest4
{
    [TestMethod]
    public void Sample()
    {
        var lines = File.ReadAllLines("./../../../../Day04/sample.txt");
        var cards = new List<Card>();
        foreach (var line in lines)
        {
            cards.Add(new Card(line));
        }
        Assert.AreEqual(13, cards.Sum(c => c.GetPoints()));
    }
    
    [TestMethod]
    public void Advanced()
    {
        var lines = File.ReadAllLines("./../../../../Day04/sample.txt");
        var game = new Game(lines);
        Assert.AreEqual(30, game.Result.Count() + game.Cards.Count());
        Assert.AreEqual(1, game.Result.Count(c => c.Id == 2));
        Assert.AreEqual(3, game.Result.Count(c => c.Id == 3));
        Assert.AreEqual(7, game.Result.Count(c => c.Id == 4));
        Assert.AreEqual(13, game.Result.Count(c => c.Id == 5));
        Assert.AreEqual(0, game.Result.Count(c => c.Id == 6));
    }

    [TestMethod]
    public void Advanced2()
    {
        var lines = File.ReadAllLines("./../../../../Day04/sample.txt");
        var cards = new List<Card>();
        foreach (var line in lines)
        {
            cards.Add(new Card(line));
        }
        var game = Game.CardWinnings2(cards);
        Assert.AreEqual(30, game.Sum(c => c.Amount));
        Assert.AreEqual(2, game.First(c => c.Id == 2).Amount);
        Assert.AreEqual(4, game.First(c => c.Id == 3).Amount);
        Assert.AreEqual(8, game.First(c => c.Id == 4).Amount);
        Assert.AreEqual(14, game.First(c => c.Id == 5).Amount);
        Assert.AreEqual(1, game.First(c => c.Id == 6).Amount);
    }
}
