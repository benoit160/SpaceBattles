using SpaceBattles.Core.Domain.Entities.Universe;

namespace SpaceBattles.Tests.Domain.Building;

public class BuildingLevelTests
{
    [Fact]
    public void NewPlanet_Building_OperatingLevel100()
    {
        // Arrange
        Planet planet = new Planet();

        // Act
        bool allAt100 = planet.Buildings.All(b => b.OperatingLevel == 100);

        // Assert
        Assert.True(allAt100);
    }
}