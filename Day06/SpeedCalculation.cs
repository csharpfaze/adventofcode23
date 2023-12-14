
namespace Day06;

public static class SpeedCalculation
{
    public static int Range(int raceDuration, int holdMs)
    {
        if (raceDuration < holdMs)
            return 0;
        var speedPerMs = holdMs;
        var movementDuration = raceDuration - holdMs;
        return movementDuration * speedPerMs;
    }

    public static int Margin(IEnumerable<int> possibleRecords)
    {
        var margin = 1;
        foreach (var record in possibleRecords)
        {
            if (record != 0)
                margin *= record;
        }
        return margin;
    }

    public static IEnumerable<int> GetPossibilites(Race race)
    {
        var records = new List<int>();
        foreach (var round in race.Rounds)
        {
            var record = 0;
            for (int i = 0; i < round.Time; i++)
            {
                if (Range(round.Time, i) > round.Distance)
                    record += 1;
            }
            records.Add(record);
        }
        return records;
    }
}