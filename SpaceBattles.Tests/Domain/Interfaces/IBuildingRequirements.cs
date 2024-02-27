using SpaceBattles.Core.Domain.Entities.Building;
using SpaceBattles.Core.Domain.Entities.Universe;
using SpaceBattles.Core.Domain.Records;

namespace SpaceBattles.Tests.Domain.Interfaces;

public class BuildingRequirements
{
    [Fact]
    public void NoRequirements()
    {
        // Arrange
        Planet planet = new Planet();
        Building building = Building.Buildings().First();

        // Act
        bool success = planet.MeetsBuildingRequirements(building);

        // Assert
        Assert.True(success);
    }
    
    [Fact]
    public void RequirementsWithLevelZero()
    {
        // Arrange
        Planet planet = new Planet();
        Building building = new Building
        {
            BuildingRequirements = 
            [
                new BuildingRequirement(1, 0),
                new BuildingRequirement(4, 0),
                new BuildingRequirement(7, 0),
            ]
        };

        // Act
        bool success = planet.MeetsBuildingRequirements(building);

        // Assert
        Assert.True(success);
    }
    
    [Fact]
    public void OneRequirementNotMet()
    {
        // Arrange
        Planet planet = new Planet();
        Building building = new  Building
        {
            BuildingRequirements = 
            [
                new BuildingRequirement(1, 1),
            ]
        };

        // Act
        bool success = planet.MeetsBuildingRequirements(building);

        // Assert
        Assert.False(success);
    }
    
    [Fact]
    public void OneRequirementMet()
    {
        // Arrange
        Planet planet = new Planet();
        planet.Buildings.ForEach(x => x.Level++);
        
        Building building = new  Building
        {
            BuildingRequirements = 
            [
                new BuildingRequirement(1, 1),
            ]
        };

        // Act
        bool success = planet.MeetsBuildingRequirements(building);

        // Assert
        Assert.True(success);
    }
}