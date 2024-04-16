using SpaceBattles.Core.Domain.Entities.Universe;

namespace SpaceBattles.Tests.Domain.Upgrade;

public class BuildingUpgradeTests
{
    const short InvalidId = 17;

    [Theory]
    [InlineData(1, true)]
    [InlineData(2, false)]
    [InlineData(3, true)]
    [InlineData(4, false)]
    public void CanUpgradeBuilding(short buildingId, bool expectedResult)
    {
        // Arrange
        Planet planet = new Planet();
        planet.Init();
        
        // Act
        bool succeeded = planet.CanUpgradeBuilding(buildingId);
        
        // Assert
        Assert.Equal(expectedResult, succeeded);
    }
    
    [Fact]
    public void CanUpgradeBuilding_InvalidId()
    {
        // Arrange
        Planet planet = new Planet();
        planet.Init();
        
        // Act
        bool succeeded = planet.CanUpgradeBuilding(InvalidId);
        
        // Assert
        Assert.False(succeeded);
    }
    
    [Fact]
    public void TryUpgradeBuilding()
    {
        // Arrange
        Planet planet = new Planet();
        planet.Init();
        
        // Act
        bool succeeded = planet.TryUpgradeBuilding(1);
        
        // Assert
        Assert.True(succeeded);
        Assert.NotNull(planet.BuildingUpgrade);
        Assert.Equal(108, planet.BuildingUpgrade.Duration.TotalSeconds);
    }
    
    [Fact]
    public void TryUpgradeBuilding_Fail_InvalidId()
    {
        // Arrange
        Planet planet = new Planet();
        planet.Init();
        
        // Act
        bool succeeded = planet.TryUpgradeBuilding(InvalidId);
        
        // Assert
        Assert.False(succeeded);
        Assert.Null(planet.BuildingUpgrade);
    }
    
    [Fact]
    public void TryUpgradeBuilding_Fail_NotEnoughResources()
    {
        // Arrange
        Planet planet = new Planet();
        planet.Init();
        
        // Act
        bool succeeded = planet.TryUpgradeBuilding(2);
        
        // Assert
        Assert.False(succeeded);
        Assert.Null(planet.BuildingUpgrade);
    }

    [Fact]
    public void ProcessUpgrade()
    {
        // Arrange
        Planet planet = new Planet();
        planet.Init();

        // Act
        planet.TryUpgradeBuilding(1);
        planet.ProcessUpgrades(DateTime.Now + TimeSpan.FromSeconds(120));
        
        // Assert
        Assert.Null(planet.BuildingUpgrade);
        Assert.Equal(1, planet.Buildings.First().Level);
    }
    
    [Fact]
    public void ProcessUpgradeNotFinished()
    {
        // Arrange
        Planet planet = new Planet();
        planet.Init();

        // Act
        planet.TryUpgradeBuilding(1);
        planet.ProcessUpgrades(DateTime.Now + TimeSpan.FromSeconds(60));
        
        // Assert
        Assert.NotNull(planet.BuildingUpgrade);
        Assert.Equal(0, planet.Buildings.First().Level);
    }
}