// See https://aka.ms/new-console-template for more information
using SnakesAndLaddersSimulator;

Console.WriteLine("Beginning");

var game = new GameBoard(1);

const uint MaxRolls = 1000;
uint rolls = 0;
bool finished = true;

while(!game.Roll())
{
    if(++rolls >= MaxRolls)
    {
        Console.WriteLine("Game took too long");
        break;
    }
}

var history = game.Players[0].History;

foreach (var item in history)
{
    Console.WriteLine($"Roll: {item.Roll}, Position: {item.Position}");
}

Console.WriteLine($"Number of rolls: {history.Count()}");

Console.WriteLine(finished ? "Done" : "Game took too long");

//Console.Write("\rHello again!");