using System.Text.Json.Serialization;
using SpaceBattles.Core.Application.Extensions.SuperLinq;
using SpaceBattles.Core.Domain.Entities.Universe;
using SpaceBattles.Core.Domain.Enums;

namespace SpaceBattles.Core.Domain.Entities.Battle;

public class Fleet
{
    [JsonIgnore]
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
    public static bool CreateFleet(this Planet planet, int fuel)
    {
        planet[Resource.Helium] -= fuel;

        Fleet fleet = new Fleet()
        {
            Owner = planet.Owner,
            OwnerId = planet.OwnerId.Value,
            Position = new Position(planet.Galaxy, planet.SolarSystem, planet.Slot),
        };
    }
}