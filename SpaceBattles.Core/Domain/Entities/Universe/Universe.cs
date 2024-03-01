using SpaceBattles.Core.Domain.Models;

namespace SpaceBattles.Core.Domain.Entities.Universe;

public sealed class Universe
{
    public required string Name { get; set; }

    public DateTime CreationDate { get; set; }
        = DateTime.Now;

    public bool IsPeacefulUniverse { get; set; }

    public float UniverseSpeed { get; set; }

    public List<Planet> Planets { get; set; }
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