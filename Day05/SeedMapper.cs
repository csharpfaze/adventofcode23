
namespace Day05;

public record Mapping(long DestinationIndex, long SourceIndex, long Range);

public class SeedMapper
{
    public IEnumerable<long> Seeds { get; set; }
    public IEnumerable<long> SeedLine { get; set; }
    public List<Mapping> Soils { get; set; } = new();
    public List<Mapping> Fertilizers { get; set; } = new();
    public List<Mapping> Water { get; set; } = new();
    public List<Mapping> Lights { get; set; } = new();
    public List<Mapping> Temperatures { get; set; } = new();
    public List<Mapping> Humidities { get; set; } = new();
    public List<Mapping> Locations { get; set; } = new();

    public SeedMapper(IEnumerable<string> lines, bool seedPairs = false)
    {
        var seeds = lines.ElementAt(0).Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => Convert.ToInt64(s));
        if (!seedPairs)
        {
            Seeds = seeds;
        }
        else
        {
            SeedLine = seeds;
        }
        Map(lines);
    }

    public IEnumerable<long> GetSeedRangeFromPairs(IEnumerable<long> seeds)
    {
        var createdSeeds = new List<long>();
        for (int i = 0; i <= seeds.Count() - 2; i = i + 2)
        {
            createdSeeds.AddRange(CreateSeeds(seeds.ElementAt(i), seeds.ElementAt(i + 1)));
        }
        return createdSeeds;
    }

    private IEnumerable<long> CreateSeeds(long start, long range)
    {
        var seeds = new List<long>();
        for (long i = start; i < start + range; i++)
        {
            seeds.Add(i);
        }
        return seeds;
    }

    private void Map(IEnumerable<string> lines)
    {

        List<Mapping> category = Soils;

        for (int i = 1; i < lines.Count(); i++)
        {
            var line = lines.ElementAt(i);

            if (string.IsNullOrEmpty(line))
                continue;

            if (line.Contains(':'))
            {
                category = Category(line);
            }
            else
            {
                var map = line.Split(' ').Select(x => Convert.ToInt64(x));
                category.Add(new Mapping(map.ElementAt(0), map.ElementAt(1), map.ElementAt(2)));
            }
        }
    }

    private List<Mapping> Category(string line) =>
    line switch
    {
        "seed-to-soil map:" => Soils,
        "soil-to-fertilizer map:" => Fertilizers,
        "fertilizer-to-water map:" => Water,
        "water-to-light map:" => Lights,
        "light-to-temperature map:" => Temperatures,
        "temperature-to-humidity map:" => Humidities,
        _ => Locations
    };

    public IEnumerable<long> CreateSeedChain(long seed)
    {
        var chain = new List<long>
        {
            seed
        };
        var soil = GetValue(Soils, seed);
        chain.Add(soil);
        var fertilizer = GetValue(Fertilizers, soil);
        chain.Add(fertilizer);
        var water = GetValue(Water, fertilizer);
        chain.Add(water);
        var light = GetValue(Lights, water);
        chain.Add(light);
        var temperature = GetValue(Temperatures, light);
        chain.Add(temperature);
        var humidity = GetValue(Humidities, temperature);
        chain.Add(humidity);
        var location = GetValue(Locations, humidity);
        chain.Add(location);
        return chain;
    }

    private long GetValue(IEnumerable<Mapping> mappings, long source)
    {
        foreach (var mapping in mappings)
        {
            if (source >= mapping.SourceIndex && source <= mapping.SourceIndex + mapping.Range - 1)
            {
                return mapping.DestinationIndex - mapping.SourceIndex + source;
            }
        }
        return source;
    }
}
