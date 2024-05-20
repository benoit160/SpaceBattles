using SpaceBattles.Core.Domain.Entities.Battle;
using SpaceBattles.Core.Domain.Entities.Universe;
using SpaceBattles.Core.Domain.Enums;
using SpaceBattles.Core.Domain.Models;

namespace SpaceBattles.Tests.Domain.Universe;

public class UniverseTests
{
    [Fact]
    public void NoDuplicatePlanets()
    {
        // Arrange
        UniverseCreationModel model = new UniverseCreationModel
        {
            UniverseSize = UniverseSize.VeryLarge,
        };

        // Act
        Core.Domain.Entities.Universe.Universe universe = Core.Domain.Entities.Universe.Universe.CreateUniverse(model);
        
        // Assert
        foreach(var grouping in universe.Planets.GroupBy(planet => (planet.Galaxy, planet.SolarSystem)))
        {
            HashSet<byte> images = grouping.Select(x => x.ImageIndex).ToHashSet();
            Assert.Equal(grouping.Select(x => x.ImageIndex).Count(), images.Count);
        }
    }

    [Fact]
    public void FleetOrbitingPlanet()
    {
        // Arrange
        Core.Domain.Entities.Universe.Universe universe = Core.Domain.Entities.Universe.Universe.CreateUniverse(new UniverseCreationModel());
        Planet planet = universe.Planets[Random.Shared.Next(universe.Planets.Length)];
        Fleet fleet = new Fleet
        {
            Position = new Position(planet.Galaxy, planet.SolarSystem, planet.Slot),
        };
        universe.Fleets.Add(fleet);

        // Act
        Fleet? result = universe.FleetOrbitingPlanet(planet);

        // Assert
        Assert.NotNull(result);
        Assert.Same(result, fleet);
    }
}