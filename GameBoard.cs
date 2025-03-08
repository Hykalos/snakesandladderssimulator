namespace SnakesAndLaddersSimulator;

internal class GameBoard
{
    public readonly Player[] Players;
    private const uint StartingPosition = 0;
    private const uint FinishPosition = 100;
    /// <summary>
    /// Go back again if false if you roll higher than the finish position
    /// </summary>
    private const bool AllowRollover = true;

    private uint _currentPlayer = 0;

    private readonly IReadOnlyDictionary<uint, uint> SnakesAndLadders = new Dictionary<uint, uint>
    {
        { 1, 38 },
        { 6, 16 },
        { 11, 49 },
        { 14, 4 },
        { 21, 60 },
        { 24, 87 },
        { 31, 9 },
        { 35, 47 },
        { 44, 26 },
        { 51, 67 },
        { 56, 53 },
        { 62, 19 },
        { 64, 42 },
        { 73, 93 },
        { 78, 100 },
        { 84, 28 },
        { 91, 71 },
        { 95, 75 },
        { 98, 80 }
    };

    public GameBoard(int numberOfPlayers)
    {
        Players = new Player[numberOfPlayers];

        Parallel.For(0, numberOfPlayers, (int i) =>
        {

            Players[i] = new Player(StartingPosition);
        }
        );
    }

    public IEnumerable<(uint PlayerId, uint Position, bool Finished)> GetGameStatus() => Players.Select((p, i) => ((uint)i + 1, p.Position, p.Position >= FinishPosition));

    public bool Roll()
    {
        var roll = Dice.RoleDice();
        var player = Players[_currentPlayer];
        uint newPosition = player.Position + roll;
        if (newPosition > FinishPosition && !AllowRollover)
        {
#pragma warning disable CS0162 // Unreachable code detected
            newPosition = FinishPosition - (newPosition - FinishPosition);
#pragma warning restore CS0162 // Unreachable code detected
        }
        if (SnakesAndLadders.TryGetValue(newPosition, out var newPositionAfterSnakesAndLadders))
        {
            newPosition = newPositionAfterSnakesAndLadders;
        }
        player.NewPosition(roll, newPosition);

        ++_currentPlayer;

        if (_currentPlayer >= Players.Length)
        {
            _currentPlayer = 0;
        }

        return player.Position >= FinishPosition;
    }
}
