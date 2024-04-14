using SpaceBattles.Core.Domain.Entities.Universe;

namespace SpaceBattles.Tests.Domain.Buildings;

public class BuildingLevelTests
{
    [Fact]
    public void NewPlanet_Building_OperatingLevel100()
    {
        // Arrange
        Planet planet = new Planet();
        planet.Init();

        // Act
        bool allAt100 = Array.TrueForAll(planet.Buildings, b => b.OperatingLevel == 100);

        // Assert
        Assert.True(allAt100);
    }
    
    [Fact]
    public void AllBuilding_LevelZero_ZeroEnergy()
    {
        // Arrange
        Planet planet = new Planet();
        planet.Init();

        // Act
        bool allAtZero = Array.TrueForAll(planet.Buildings, b => b.Energy == 0);
        
        // Assert
        Assert.True(allAtZero);
    }
    
    [Theory]
    [InlineData(1, -11)]
    [InlineData(2, -24)]
    [InlineData(3, -40)]
    [InlineData(4, -59)]
    [InlineData(5, -81)]
    public void MetalMine_Consumption(short level, int expectedConsumption)
    {
        // Arrange
        Planet planet = new Planet();
        planet.Init();
        planet.Buildings[0].Level = level;

        // Act
        int actualEnergy = planet.Buildings[0].Energy;
        
        // Assert
        Assert.Equal(expectedConsumption, actualEnergy);
    }
    
    [Theory]
    [InlineData(1, 22)]
    [InlineData(2, 48)]
    [InlineData(3, 80)]
    [InlineData(4, 117)]
    [InlineData(5, 161)]
    public void SolarPlant_Production(short level, int expectedConsumption)
    {
        // Arrange
        Planet planet = new Planet();
        planet.Init();
        planet.Buildings[6].Level = level;

        // Act
        int actualEnergy = planet.Buildings[6].Energy;
        
        // Assert
        Assert.Equal(expectedConsumption, actualEnergy);
    }
    
    [Theory]
    [InlineData(1, 0)]
    [InlineData(2, 0)]
    public void OtherBuilding_NoEnergyUsage(short level, int expectedConsumption)
    {
        // Arrange
        Planet planet = new Planet();
        planet.Init();
        planet.Buildings[8].Level = level;

        // Act
        int actualEnergy = planet.Buildings[8].Energy;
        
        // Assert
        Assert.Equal(expectedConsumption, actualEnergy);
    }
}