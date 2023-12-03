
using System.Drawing;

public class GameRecord
{
    public int Id { get; set; }

    public List<Draw> Draws { get; set; } = new();

    public GameRecord(string record)
    {
        ParseRecord(record);
    }

    private void ParseRecord(string record)
    {
        //Game 1:
        var gameId = Convert.ToInt32(record.Split(':')[0].Replace("Game ", ""));
        Id = gameId;
        //3 blue, 4 red; ...
        var draws = record.Split(':')[1].Split("; ");
        foreach (var draw in draws)
        {
            Draws.AddRange(ParseDraw(draw.Trim()));
        }
    }

    private IEnumerable<Draw> ParseDraw(string draws)
    {
        //3 blue, 4 red
        var list = new List<Draw>();
        foreach (var draw in draws.Split(", "))
        {
            var number = Convert.ToInt32(draw.Split(" ")[0]);
            var color = draw.Split(" ")[1];
            list.Add(CreateDraw(color, number));
        }
        return list;
    }

    private static Draw CreateDraw(string color, int number)
    {
        switch (color)
        {
            case "red":
                return new Draw(number, Color.Red);
            case "green":
                return new Draw(number, Color.Green);
            case "blue":
                return new Draw(number, Color.Blue);
            default:
                throw new ArgumentException();
        }
    }

    public bool IsPossible(IEnumerable<Draw> bag)
    {
        foreach (var draw in Draws)
        {
            var bagColor = bag.First(d => d.Color == draw.Color);
            if (draw.Amount > bagColor.Amount)
                return false;
        }
        return true;
    }

    public IEnumerable<Draw> AtLeastNecessary()
    {
        var reds = Draws.Where(d => d.Color == Color.Red).Max(d => d.Amount);
        var greens = Draws.Where(d => d.Color == Color.Green).Max(d => d.Amount);
        var blues = Draws.Where(d => d.Color == Color.Blue).Max(d => d.Amount);
        return new List<Draw>() { new Draw(reds, Color.Red), new Draw(greens, Color.Green), new Draw(blues, Color.Blue)};
    }

    public static int GetPower(IEnumerable<Draw> draws)
    {
        return draws.ElementAt(0).Amount * draws.ElementAt(1).Amount * draws.ElementAt(2).Amount;
    }
}