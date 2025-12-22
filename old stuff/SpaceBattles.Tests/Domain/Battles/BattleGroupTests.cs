using SpaceBattles.Core.Domain.Entities.Battle;
using SpaceBattles.Core.Domain.Entities.Universe;
using SpaceBattles.Core.Domain.ValueTypes;

namespace SpaceBattles.Tests.Domain.Battles;

public class BattleGroupTests
{
    [Fact]
    public void EmptyBattleGroup()
    {
        // Arrange
        Planet planet = new Planet();
        planet.Init();

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
        planet.Init();
        planet.BattleUnits[0].Quantity = count;

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
        planet.Init();

        planet.BattleUnits[0].Quantity = defenseCount;
        planet.BattleUnits[8].Quantity = spaceshipCount;

        // Act
        BattleGroup battleGroup = new BattleGroup(planet);

        // Assert
        Assert.Equal(spaceshipCount + defenseCount, battleGroup.Length);
        Assert.True(battleGroup.IsAlive);
    }
    
    [Fact]
    public void OneOfEach()
    {
        // Arrange
        Planet planet = new Planet();
        planet.Init();

        Array.ForEach(planet.BattleUnits, b => b.Quantity = 1);

        // Act
        BattleGroup battleGroup = new BattleGroup(planet);

        // Assert
        int expectedCount = Spaceship.Spaceships().Length + Defense.Defenses().Length;
        
        Assert.Equal(expectedCount, battleGroup.Length);
        Assert.True(battleGroup.IsAlive);
    }
}