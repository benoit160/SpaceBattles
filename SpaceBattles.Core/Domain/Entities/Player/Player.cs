namespace SpaceBattles.Core.Domain.Entities.Player;

public sealed class Player
{
    public short Id { get; set; }

    public required string Name { get; init; }

    public bool IsBot { get; set; }
}