using SpaceBattles.Core.Domain.Entities.Building;
using SpaceBattles.Core.Domain.Entities.Universe;
using SpaceBattles.Core.Domain.Enums;
using SpaceBattles.Core.Domain.Interfaces;

namespace SpaceBattles.Tests.Domain.Interfaces;

public class RequirementsTests
{
    private const int StartingTitanium = 150;
    private const int StartingSilicon = 75;
    private const int StartingHelium = 0;
    
    [Theory]
    [InlineData(1, true)]
    [InlineData(2, false)]
    [InlineData(3, true)]
    [InlineData(4, false)]
    [InlineData(5, false)]
    [InlineData(6, false)]
    [InlineData(7, true)]
    [InlineData(8, false)]
    [InlineData(9, false)]
    [InlineData(10, false)]
    [InlineData(11, false)]
    [InlineData(12, false)]
    public void HasEnoughResources_Building(short buildingId, bool expectedResult)
    {
        // Arrange
        Planet planet = new Planet();
        planet.Init();
        IRequirements building = planet.Buildings.First(b => b.BuildingId == buildingId);

        // Act
        bool sufficient = planet.HasEnoughResource(building);
        
        // Asset 
        Assert.Equal(expectedResult, sufficient);
    }
    
    [Fact]
    public void ConsumeResources_Building()
    {
        // Arrange
        Planet planet = new Planet();
        planet.Init();
        IRequirements building = planet.Buildings.First();

        // Act
        planet.ConsumeResources(building);
        
        // Asset 
        Assert.Equal(StartingTitanium - building.TitaniumCost, planet[Resource.Titanium]);
        Assert.Equal(StartingSilicon - building.SiliconCost, planet[Resource.Silicon]);
        Assert.Equal(StartingHelium - building.HeliumCost, planet[Resource.Helium]);
    }

    [Theory]
    [InlineData(0, 1e6, 5e5, 1e5)]
    [InlineData(1, 2e6, 1e6, 2e5)]
    [InlineData(2, 4e6, 2e6, 4e5)]
    public void CostScaling(short level, int titanium, int silicon, int helium)
    {
        // Arrange
        Planet planet = new Planet();
        planet.Init();
        BuildingLevel building = planet.Buildings.First(b => b.BuildingId == 11);
        building.Level = level;
        
        // Assert
        Assert.Equal(titanium, building.TitaniumCost);
        Assert.Equal(silicon, building.SiliconCost);
        Assert.Equal(helium, building.HeliumCost);
    }
}