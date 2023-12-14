using Day06;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var race = new Race(File.ReadAllLines("./puzzle_input.txt"));

System.Console.WriteLine(SpeedCalculation.Margin(SpeedCalculation.GetPossibilites(race)));