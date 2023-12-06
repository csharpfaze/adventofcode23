// See https://aka.ms/new-console-template for more information
using Day05;

Console.WriteLine("Hello, World!");


var lines = File.ReadAllLines("./puzzle_input.txt");
var mapper = new SeedMapper(lines);

System.Console.WriteLine("Min Location:");
System.Console.WriteLine(mapper.Seeds.Min(seed => mapper.CreateSeedChain(seed).Last()));