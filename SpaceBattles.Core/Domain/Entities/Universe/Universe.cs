using SpaceBattles.Core.Domain.Models;

namespace SpaceBattles.Core.Domain.Entities.Universe;

public sealed class Universe
{
    public required string Name { get; init; }

    public DateTime CreationDate { get; init; }
        = DateTime.Now;

    public bool IsPeacefulUniverse { get; init; }

    public float UniverseSpeed { get; init; }

    public List<Planet> Planets { get; init; }
        = new();

    public Planet this[int index]
        => Planets[index];

    public static Universe CreateUniverse(UniverseCreationModel model)
    {
        Universe newUniverse = new Universe()
        {
            Name = model.UniverseName,
            IsPeacefulUniverse = model.IsPeacefulMode,
            UniverseSpeed = model.UniverseSpeed,
        };

        newUniverse.Planets.AddRange(Enumerable
            .Range(0, model.NumberOfPlanets)
            .Select(_ => new Planet()));

        return newUniverse;
    }
}