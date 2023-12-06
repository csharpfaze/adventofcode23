// See https://aka.ms/new-console-template for more information
using Day05;

Console.WriteLine("Hello, World!");


var lines = File.ReadAllLines("./puzzle_input.txt");
var mapper = new SeedMapper(lines);

System.Console.WriteLine("Min Location:");
System.Console.WriteLine(mapper.Seeds.Min(seed => mapper.CreateSeedChain(seed).Last()));


var mapperWithRanges = new SeedMapper(lines, true);
System.Console.WriteLine("Min Location with seed ranges:  (calculation > 29 minutes)");

var minLocations = new List<long>();

for (int i = 0; i < mapperWithRanges.SeedLine.Count() - 2; i = i + 2)
{
    mapperWithRanges.Seeds = mapperWithRanges.GetSeedRangeFromPairs(new[] { mapperWithRanges.SeedLine.ElementAt(i), mapperWithRanges.SeedLine.ElementAt(i + 1) });
    var locations = new List<long>();
    foreach (var seed in mapperWithRanges.Seeds)
    {
        locations.Add(mapperWithRanges.CreateSeedChain(seed).Last());
    }
    minLocations.Add(locations.Min());
}

System.Console.WriteLine(minLocations.Min());

