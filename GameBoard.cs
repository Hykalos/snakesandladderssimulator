namespace SnakesAndLaddersSimulator;

internal class GameBoard
{
    public readonly Player[] Players;

    public GameBoard(int numberOfPlayers)
    {
        Players = new Player[numberOfPlayers];

        Parallel.For(0, numberOfPlayers, (int i) =>
        {

            Players[i] = new Player();
        }
        );
    }
}
