namespace SpaceBattles.Core.Domain.Entities.Universe;

using SpaceBattles.Core.Domain.Enums;
using SpaceBattles.Core.Domain.Models;

public sealed class Universe
{
    public required string Name { get; init; }

    public DateTime CreationDate { get; init; }
        = DateTime.Now;

    public bool IsPeacefulUniverse { get; init; }

    public float UniverseSpeed { get; init; }

    public int Galaxies { get; init; }

    public int SolarSystems { get; init; }

    public int Slots { get; init; }

    public Planet[] Planets { get; init; }
        = Array.Empty<Planet>();

    public List<Player.Player> Players { get; init; }
        = new();

    public Planet this[int index]
        => Planets[index];

    public static Universe CreateUniverse(UniverseCreationModel model)
    {
        (byte galaxies, byte solarSystems, byte slots) = GetSize(model.UniverseSize);

        Universe newUniverse = new Universe()
        {
            Name = model.UniverseName,
            IsPeacefulUniverse = model.IsPeacefulMode,
            UniverseSpeed = model.UniverseSpeed,
            Planets = new Planet[galaxies * solarSystems * slots],
            Galaxies = galaxies,
            SolarSystems = solarSystems,
            Slots = slots,
        };

        int index = 0;
        Span<byte> usedPlanetIndexes = stackalloc byte[slots];

        for (byte gal = 1; gal <= galaxies; gal++)
        {
            for (byte sol = 1; sol <= solarSystems; sol++)
            {
                for (byte slot = 1; slot <= slots; slot++)
                {
                    byte newImageIndex = 0;

                    // loop until a new random value is found that doesn't duplicate index for this solar system
                    while (usedPlanetIndexes.Contains(newImageIndex))
                    {
                        newImageIndex = Convert.ToByte(Random.Shared.Next(0, 14));
                    }

                    newUniverse.Planets[index] = new Planet()
                    {
                        ImageIndex = newImageIndex,
                        Galaxy = gal,
                        SolarSystem = sol,
                        Slot = slot,
                    };

                    usedPlanetIndexes[slot - 1] = newImageIndex;
                    index++;
                }
            }
        }

        Player.Player mainPlayer = new Player.Player()
        {
            Id = 1,
            IsBot = false,
            Name = model.CommanderName,
        };

        newUniverse.Players.Add(mainPlayer);

        Planet startingPlanet = newUniverse.Planets[Random.Shared.Next(0, newUniverse.Planets.Length)];
        startingPlanet.DefineOwner(mainPlayer);
        startingPlanet.Init();
        startingPlanet.Name = model.StartingPlanetName;

        return newUniverse;
    }

    public Memory<Planet> GetSolarSystemView(int galaxy, int solarSystem)
    {
        int totalSlotsPerGalaxy = SolarSystems * Slots;
        int start = (totalSlotsPerGalaxy * (galaxy - 1)) + ((solarSystem - 1) * Slots);
        return Planets.AsMemory().Slice(start, Slots);
    }

    private static (byte Slots, byte SolarSystems, byte Galaxies) GetSize(UniverseSize size)
    {
        return size switch
        {
            UniverseSize.VerySmall => (1, 1, 4),
            UniverseSize.Small => (1, 3, 4),
            UniverseSize.Medium => (2, 4, 4),
            UniverseSize.Large => (3, 5, 5),
            UniverseSize.VeryLarge => (4, 7, 5),
            _ => throw new ArgumentOutOfRangeException(nameof(size), size, null),
        };
    }
}