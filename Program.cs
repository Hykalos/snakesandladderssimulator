// See https://aka.ms/new-console-template for more information
using SnakesAndLaddersSimulator;

Console.WriteLine("Beginning");

const uint Simulations = 1000000;
const uint MaxRolls = 1000;
uint finishedGames = 0;

var gameBoards = new GameBoard[Simulations];

for (uint i = 0; i < Simulations; i++)
{
    gameBoards[i] = new GameBoard(1);
}

var tasks = gameBoards.Select(gb => RunGame(gb));
UpdateProgress(finishedGames, Simulations);

await foreach(var task in Task.WhenEach(tasks))
{
    ++finishedGames;
    UpdateProgress(finishedGames, Simulations);
}

Console.WriteLine();

//var history = game.Players[0].History;

//foreach (var item in history)
//{
//    Console.WriteLine($"Roll: {item.Roll}, Position: {item.Position}");
//}

//Console.WriteLine($"Number of rolls: {history.Count()}");

//Console.Write("\rHello again!");

void UpdateProgress(uint finishedGames, uint totalGames)
{
    Console.Write($"\r{finishedGames} of {totalGames} games finished");
}

Task RunGame(GameBoard gameBoard)
{
    return Task.Run(() =>
     {
         uint rolls = 0;

         while (!gameBoard.Roll())
         {
             if (++rolls >= MaxRolls)
             {
                 Console.WriteLine("Game took too long");
                 break;
             }
         }
     });
}