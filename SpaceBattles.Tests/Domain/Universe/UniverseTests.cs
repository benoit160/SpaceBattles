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

        // Act

        // Assert
    }
}