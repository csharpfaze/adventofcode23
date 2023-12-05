// See https://aka.ms/new-console-template for more information
using Day04;

Console.WriteLine("Hello, World!");

var lines = File.ReadAllLines("./puzzle_input.txt");

var cards = new List<Card>();
foreach (var line in lines)
{
    cards.Add(new Card(line));
}

System.Console.WriteLine("Points: " + cards.Sum(c => c.GetPoints()));

var game = new Game(lines);

System.Console.WriteLine($"Collected Cards: { game.Result.Count() + game.Cards.Count()}");

//non rec approach
//System.Console.WriteLine("Collected Cards: " + game.Result.Sum(r => r.Amount));