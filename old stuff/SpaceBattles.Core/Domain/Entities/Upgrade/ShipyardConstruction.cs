namespace SpaceBattles.Core.Domain.Entities.Upgrade;

public sealed class ShipyardConstruction : Upgrade
{
    public short CombatEntityId { get; init; }

    public short Quantity { get; init; }
}