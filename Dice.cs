namespace SnakesAndLaddersSimulator;

internal static class Dice
{
    private const uint MaxRoll = 6;

    public static uint RoleDice()
        => (uint)Random.Shared.Next(0, (int)MaxRoll + 1);
}
