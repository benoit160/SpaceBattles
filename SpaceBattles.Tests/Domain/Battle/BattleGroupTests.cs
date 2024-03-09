using SpaceBattles.Core.Domain.Entities.Universe;
using SpaceBattles.Core.Domain.ValueTypes;

namespace SpaceBattles.Tests.Domain.Battle;

public class BattleGroupTests
{
    [Fact]
    public void EmptyBattleGroup()
    {
        // Arrange
        Planet planet = new Planet();

        // Act
        BattleGroup battleGroup = new BattleGroup(planet);

        // Assert
        Assert.Equal(0, battleGroup.Length);
    }
    
    [Theory]
    [InlineData(1)]
    [InlineData(25)]
    public void SingeUnit(int count)
    {
        // Arrange
        Planet planet = new Planet();
        planet.Spaceships[0].Quantity = count;
        
        // Act
        BattleGroup battleGroup = new BattleGroup(planet);

        // Assert
        Assert.Equal(count, battleGroup.Length);
        Assert.True(battleGroup.IsAlive);
    }
    
    [Theory]
    [InlineData(1, 3)]
    [InlineData(25, 75)]
    public void SingeUnit_SpaceshipAndDefense(int spaceshipCount, int defenseCount)
    {
        // Arrange
        Planet planet = new Planet();
        planet.Spaceships[0].Quantity = spaceshipCount;
        planet.Defenses[0].Quantity = defenseCount;
        
        // Act
        BattleGroup battleGroup = new BattleGroup(planet);

        // Assert
        Assert.Equal(spaceshipCount + defenseCount, battleGroup.Length);
        Assert.True(battleGroup.IsAlive);
    }
}