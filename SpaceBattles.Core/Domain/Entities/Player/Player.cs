namespace SpaceBattles.Core.Domain.Entities.Player;

public sealed class Player
{
    public required string Name { get; init; }

    public bool IsBot { get; set; }
}