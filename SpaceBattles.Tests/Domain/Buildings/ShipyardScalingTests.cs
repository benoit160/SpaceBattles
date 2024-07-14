using SpaceBattles.Core.Domain.Entities.Battle;
using SpaceBattles.Core.Domain.Entities.Building;
using SpaceBattles.Core.Domain.Entities.Universe;
using SpaceBattles.Core.Domain.Interfaces;

namespace SpaceBattles.Tests.Domain.Buildings;

public class ShipyardScalingTests
{
    [Theory]
    [InlineData(0, 4000 / 2500d)]
    [InlineData(1, 4000 / 5000d)]
    [InlineData(2, 4000 / 7500d)]
    public void ShipyardScaling(short level, double expectedHours)
    {
        // Arrange
        Planet planet = new Planet();
        planet.Init();
        BuildingLevel shipyard = planet.Buildings.First(b => b.BuildingId == 9);
        shipyard.Level = level;

        // Act
        IRequirements upgrade = Spaceship.Spaceships().First();
        TimeSpan duration = upgrade.Duration(shipyard.Level);

        // Assert
        Assert.Equal(expectedHours, duration.TotalHours);
    }
}