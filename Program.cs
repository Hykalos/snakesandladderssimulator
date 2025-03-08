// See https://aka.ms/new-console-template for more information
using SnakesAndLaddersSimulator;
using System.Text;

Console.OutputEncoding = Encoding.UTF8;
Console.WriteLine("Beginning");

const uint Simulations = 1000000;
const uint MaxRolls = 10;
uint finishedGames = 0;

var gameBoards = new GameBoard[Simulations];

for (uint i = 0; i < Simulations; i++)
{
    gameBoards[i] = new GameBoard(1);
}

var tasks = gameBoards.Select(gb => RunGame(gb));

var updaterTask = ProgressUpdater();

await Parallel.ForEachAsync(gameBoards, async (gb, _) =>
{
    await RunGame(gb);
});

UpdateProgress(finishedGames, Simulations);

Console.WriteLine();
Console.WriteLine();

var lowestRolls = gameBoards.Where(gb => gb.Finished).Select(gb => gb.Players[0].History.Count()).Min();

var lowestRollingGames = gameBoards.Where(gb => gb.LowestRollsForFinishing == lowestRolls).ToArray();

Console.WriteLine($"Number of games with lowest roll count: {lowestRollingGames.Length} ({((double)lowestRollingGames.Length / Simulations * 100)}%)");
Console.WriteLine();
Console.WriteLine();

var uniqueGames = new List<IEnumerable<(uint Roll, uint Position)>>();

foreach (var game in lowestRollingGames)
{
    var playerHistory = game.Players[0].History.ToArray();
    var unique = true;

    foreach(var uniqueGame in uniqueGames)
    {
        if (playerHistory.SequenceEqual(uniqueGame))
        {
            unique = false;
            break;
        }
    }

    if(unique)
        uniqueGames.Add(playerHistory);
}

Console.WriteLine($"Lowest rolling game took {lowestRolls} rolls");

foreach(var uniqueGame in uniqueGames)
{
    Console.WriteLine("_________________________");
    Console.WriteLine("| Roll\t| Position\t|");

    foreach (var item in uniqueGame)
    {
        Console.WriteLine($"| {item.Roll}\t| {item.Position}\t\t|");
    }

    Console.WriteLine("‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾");

    Console.WriteLine();
    Console.WriteLine();
}

Task ProgressUpdater()
{
    return Task.Run(async () =>
    {
        while (finishedGames < Simulations)
        {
            UpdateProgress(finishedGames, Simulations);
            await Task.Delay(200);
        }
    });
}

void UpdateProgress(uint finishedGames, uint totalGames)
{
    var percentage = (int)((double)finishedGames / totalGames * 100);
    Console.Write($"\r{finishedGames} of {totalGames} games finished ({percentage}%)");
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
                 //Console.WriteLine("Game took too long");
                 break;
             }
         }

         Interlocked.Increment(ref finishedGames);
     });
}