using SpaceBattles.Core.Domain.Entities.Universe;

namespace SpaceBattles.Tests.Domain.Buildings;

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
    
    [Fact]
    public void AllBuilding_LevelZero_ZeroEnergy()
    {
        // Arrange
        Planet planet = new Planet();

        // Act
        bool allAtZero = planet.Buildings.All(b => b.Energy == 0);
        
        // Assert
        Assert.True(allAtZero);
    }
    
    [Theory]
    [InlineData(1, -11)]
    [InlineData(2, -25)]
    [InlineData(3, -40)]
    [InlineData(4, -59)]
    [InlineData(5, -81)]
    public void MetalMine_Consumption(short level, int expectedConsumption)
    {
        // Arrange
        Planet planet = new Planet();
        planet.Buildings[0].Level = level;

        // Act
        int actualEnergy = planet.Buildings[0].Energy;
        
        // Assert
        Assert.Equal(expectedConsumption, actualEnergy);
    }
}