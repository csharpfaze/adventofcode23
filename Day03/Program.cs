// See https://aka.ms/new-console-template for more information
using Day03;

Console.WriteLine("Hello, World!");

var lines = File.ReadAllLines("./puzzle_input.txt");

var validator = new EngineValidator(lines);

var parts = validator.GetEngineParts();

System.Console.WriteLine(parts.Sum());