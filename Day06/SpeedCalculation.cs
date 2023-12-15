
using System.Diagnostics;

namespace Day06;

public static class SpeedCalculation
{
    public static long Range(long raceDuration, long holdMs)
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

    public static IEnumerable<int> GetRacePossibilites(Race race)
    {
        var records = new List<int>();
        foreach (var round in race.Rounds)
        {
            records.Add(GetRoundPossibilty(round));
        }
        return records;
    }

    public static int GetRoundPossibilty(Round round)
    {
        var record = 0;
        for (int i = 0; i < round.Time; i++)
        {
            var range = Range(round.Time, i);
            if (range > round.Distance)
                record++;
        }

        return record;
    }
}