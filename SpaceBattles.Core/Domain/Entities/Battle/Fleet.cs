using SpaceBattles.Core.Application.Extensions.SuperLinq;
using SpaceBattles.Core.Domain.Entities.Universe;
using SpaceBattles.Core.Domain.Enums;

namespace SpaceBattles.Core.Domain.Entities.Battle;

public class Fleet
{
    public required Player.Player Owner { get; init; }

    public short OwnerId { get; init; }

    public List<CombatEntityInventory> Spaceships { get; set; }
        = new List<CombatEntityInventory>();

    public int Fuel { get; set; }

    public required Position Position { get; set; }

    public FleetAction? Action { get; set; }
}

public record struct FleetAction(DateTime Start, TimeSpan Duration, FleetActionType ActionType, Position Target);

public record struct Position(int Galaxy, int SolarSystem, int Slot);

public enum FleetActionType
{
    Idle,
    Moving,
    Attacking,
    Transporting,
    Recycling,
    Spying,
    Recall,
}

public static class PlanetFleetExtensions
{
    public static bool CreateFleet(this Planet planet, Dictionary<short, int> spaceships, int fuel)
    {
        planet[Resource.Helium] -= fuel;

        foreach (KeyValuePair<short, int> keyValuePair in spaceships)
        {
            planet.Spaceships.Span.First<CombatEntityInventory>();
        }

        ReadOnlySpan<Position> truc = stackalloc Position[10];

        truc.First()
    }
}