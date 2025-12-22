using SpaceBattles.Core.Domain.Entities.Building;
using SpaceBattles.Core.Domain.Entities.Universe;
using SpaceBattles.Core.Domain.Interfaces;

namespace SpaceBattles.Tests.Domain.Buildings;

public class RobotFactoryScalingTests
{
    [Theory]
    [InlineData(0, 75 / 2500d)]
    [InlineData(1, 75 / 5000d)]
    [InlineData(2, 75 / 7500d)]
    public void FactoryScaling(short level, double expectedHours)
    {
        // Arrange
        Planet planet = new Planet();
        planet.Init();
        BuildingLevel robotFactory = planet.Buildings.First(b => b.BuildingId == 8);
        robotFactory.Level = level;

        // Act
        IRequirements upgrade = planet.Buildings.First();
        TimeSpan duration = upgrade.Duration(robotFactory.Level);

        // Assert
        Assert.Equal(expectedHours, duration.TotalHours);
    }
}