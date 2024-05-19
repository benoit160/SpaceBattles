namespace SpaceBattles.Core.Domain.Entities.Battle;

using System.Text.Json.Serialization;
using SpaceBattles.Core.Domain.Entities.Universe;
using SpaceBattles.Core.Domain.Enums;

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

public static class PlanetFleetExtensions
{
    public static bool CreateFleet(this Planet planet, int fuel)
    {
        if (planet[Resource.Helium] < fuel) return false;

        planet[Resource.Helium] -= fuel;

        Fleet fleet = new Fleet()
        {
            Owner = planet.Owner,
            OwnerId = planet.OwnerId.Value,
            Position = new Position(planet.Galaxy, planet.SolarSystem, planet.Slot),
        };

        return true;
    }

    public bool TransferAllSpaceshipToFleet(this Planet planet, Fleet fleet)
    {
        if (fleet.Position != planet.SolarSystem 
            || fleet.Position.Galaxy != gala)
    }
}