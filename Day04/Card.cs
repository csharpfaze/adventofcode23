
namespace Day04;

public class Card
{
    public int Id { get; set; }
    /// <summary>
    /// only used for 2nd approach (non-recursive)
    /// </summary>
    public int Amount { get; set; } = 1;
    public IEnumerable<int> Winners { get; set; }
    public IEnumerable<int> Picks { get; set; }

    public Card(string line)
    {
        Parse(line);
    }

    private void Parse(string line)
    {
        Id = Convert.ToInt32(line.Split(':')[0].Replace(" ", "").Replace("Card", ""));

        var numbers = line.Split(':')[1];
        var winning = numbers.Split('|')[0].Trim().Split(' ').Where(x => !string.IsNullOrEmpty(x));
        var picks = numbers.Split('|')[1].Trim().Split(' ').Where(x => !string.IsNullOrEmpty(x));

        Winners = winning.Select(w => Convert.ToInt32(w.Trim()));
        Picks = picks.Select(p => Convert.ToInt32(p.Trim()));
    }

    public int GetPoints()
    {
        var correctNumbers = Winners.Count(w => Picks.Contains(w));
        if (correctNumbers > 0)
            return Convert.ToInt32(Math.Pow(2, correctNumbers - 1));
        return 0;
    }
}
