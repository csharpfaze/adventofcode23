namespace Day06;

public class Race
{
    public record Round(int Time, int Distance);
    public List<Round> Rounds { get; set; } = new List<Round>();
    public Race(string[] lines)
    {
        if (lines.Length != 2)
            throw new ArgumentException();

        var times = lines[0].Replace("Time:", "").Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => Convert.ToInt32(s));
        var ranges = lines[1].Replace("Distance:", "").Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => Convert.ToInt32(s));

        for (int i = 0; i < times.Count(); i++)
        {
            Rounds.Add(new Round(times.ElementAt(i), ranges.ElementAt(i)));
        }
    }
}