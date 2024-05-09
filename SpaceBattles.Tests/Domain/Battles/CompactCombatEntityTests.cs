using SpaceBattles.Core.Domain.Entities.Battle;
using SpaceBattles.Core.Domain.ValueTypes;

namespace SpaceBattles.Tests.Domain.Battles;

public class CompactCombatEntityTests
{
    [Fact]
    public void FromCombatEntity()
    {
        // Arrange
        CombatEntity lightFighter = Spaceship.Spaceships().First();

        // Act
        CompactCombatEntity fighter = new CompactCombatEntity(lightFighter);

        // Assert
        Assert.True(fighter.IsAlive);
    }
    
    [Theory]
    [InlineData(1, true)]
    [InlineData(409, true)]
    [InlineData(410, false)]
    [InlineData(5000, false)]
    public void TakeDamage(int damageCount, bool expectedToBeAlive)
    {
        // Arrange
        CombatEntity lightFighter = Spaceship.Spaceships().First();
        CompactCombatEntity fighter = new CompactCombatEntity(lightFighter);

        // Act
        fighter.TakeDamage(damageCount);

        // Assert
        Assert.Equal(expectedToBeAlive, fighter.IsAlive);
    }
}