namespace Day01;

internal class Program
{
    private static void Main(string[] args)
    {
        var total = 0;
        var lines = File.ReadAllLines("./puzzle_input.txt");
        foreach (var line in lines)
        {
            total += Parse.GetNumber(line);
        }
        System.Console.WriteLine("total: " +total);

        var advancedTotal = 0;
        foreach (var line in lines)
        {
            advancedTotal += Parse.GetNumberIncludingWritten(line);
        }
        System.Console.WriteLine("advanced logic with written numbers \r\ntotal: " +advancedTotal);
    }
}