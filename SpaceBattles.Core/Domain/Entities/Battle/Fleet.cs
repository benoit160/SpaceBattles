namespace SpaceBattles.Core.Domain.Entities.Battle;

using SpaceBattles.Core.Domain.Enums;

public class Fleet
{
    public short OwnerId { get; init; }

    public List<CombatEntityInventory> Spaceships { get; set; }
        = new List<CombatEntityInventory>();

    public int Fuel { get; set; }

    public required Position Position { get; set; }

    public FleetAction? Action { get; set; }
}

public record struct FleetAction(DateTime Start, TimeSpan Duration, FleetActionType ActionType, Position Target);

public record struct Position(int Galaxy, int SolarSystem, int Slot);