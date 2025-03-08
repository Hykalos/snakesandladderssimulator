namespace SnakesAndLaddersSimulator;

internal static class Dice
{
    private const int MaxRoll = 6;

    public static int RoleDice()
        => Random.Shared.Next(0, MaxRoll + 1);
}
