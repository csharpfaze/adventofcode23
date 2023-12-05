
namespace Day05;

public record Mapping(int DestinationIndex, int SourceIndex, int Range);

public class SeedMapper
{
    public IEnumerable<int> Seeds { get; set; }
    public List<Mapping> Soils { get; set; } = new();
    public List<Mapping> Fertilizers { get; set; } = new();
    public List<Mapping> Water { get; set; } = new();
    public List<Mapping> Lights { get; set; } = new();
    public List<Mapping> Temperatures { get; set; } = new();
    public List<Mapping> Humidities { get; set; } = new();
    public List<Mapping> Locations { get; set; } = new();

    public SeedMapper(IEnumerable<string> lines)
    {
        Map(lines);
    }

    private void Map(IEnumerable<string> lines)
    {
        Seeds = lines.ElementAt(0).Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(s => Convert.ToInt32(s));

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
                var map = line.Split(' ').Select(x => Convert.ToInt32(x));
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
}
