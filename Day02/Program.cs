using System.Drawing;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        var bag = new List<Draw>
        {
            new Draw(12, Color.Red),
            new Draw(13, Color.Green),
            new Draw(14, Color.Blue),
        };

        var lines = File.ReadLines("./../Day02/puzzle_input.txt");

        var sum = 0;

        foreach (var line in lines)
        {
            var record = new GameRecord(line);
            if (record.IsPossible(bag))
                sum += record.Id;
        }

        System.Console.WriteLine(sum);

        var powerSum = 0;

        foreach (var line in lines)
        {
            powerSum += GameRecord.GetPower(new GameRecord(line).AtLeastNecessary());
        }

        System.Console.WriteLine(powerSum);
    }
}