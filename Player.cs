using System.Collections.Immutable;

namespace SnakesAndLaddersSimulator;

public class Player
{
    public uint Position { get; private set; }

    public Player(uint startingPosition)
    {
        Position = startingPosition;
    }

    private List<(uint Roll, uint Position)> _history = new();

    public IEnumerable<(uint Roll, uint Position)> History { get => _history.ToImmutableArray(); }

    public void NewPosition(uint roll, uint newPosition)
    {
        _history.Add((roll, newPosition));
    }
}
