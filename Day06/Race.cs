namespace Day06;

public record Round(int Time, long Distance);
public class Race
{
    public List<Round> Rounds { get; set; } = new List<Round>();
    public Race(string[] lines)
    {
        if (lines.Length != 2)
            throw new ArgumentException();

        var times = lines[0].Replace("Time:", "").Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => Convert.ToInt32(s));
        var ranges = lines[1].Replace("Distance:", "").Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => Convert.ToInt64(s));

        for (int i = 0; i < times.Count(); i++)
        {
            Rounds.Add(new Round(times.ElementAt(i), ranges.ElementAt(i)));
        }
    }
}

public class Race2
{
    public Round Round { get; set; }

    public Race2(string[] lines)
    {
        var time = Convert.ToInt32(lines[0].Replace("Time:", "").Replace(" ", ""));
        var range = Convert.ToInt64(lines[1].Replace("Distance:", "").Replace(" ", ""));
        Round = new Round(time, range);
    }
}