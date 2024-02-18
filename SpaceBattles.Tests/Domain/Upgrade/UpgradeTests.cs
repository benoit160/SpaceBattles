using SpaceBattles.Core.Domain.Entities.Upgrade;

namespace SpaceBattles.Tests.Domain.Upgrade;

public class UpgradeTests
{
    public DateTime TestDate => new DateTime(1970, 1, 1, 0, 0, 0);

    [Fact]
    public void UpgradeEnd()
    {
        // Arrange
        BuildingUpgrade upgrade = new BuildingUpgrade
        {
            Start = TestDate,
            Duration = TimeSpan.FromMinutes(2),
        };

        // Assert
        Assert.Equal(TestDate + TimeSpan.FromMinutes(2), upgrade.End);
    }

    [Fact]
    public void IsStarted()
    {
        // Arrange
        BuildingUpgrade upgrade = new BuildingUpgrade
        {
            Start = TestDate,
            Duration = TimeSpan.FromMinutes(2),
        };

        // Assert
        Assert.True(upgrade.IsStarted);
    }
    
    [Fact]
    public void IsNotStarted()
    {
        // Arrange
        BuildingUpgrade upgrade = new BuildingUpgrade
        {
            Start = DateTime.Now + TimeSpan.FromMinutes(2),
            Duration = TimeSpan.FromMinutes(2),
        };

        // Assert
        Assert.False(upgrade.IsStarted);
    }

    [Fact]
    public void IsFinished()
    {
        // Arrange
        BuildingUpgrade upgrade = new BuildingUpgrade
        {
            Start = TestDate,
            Duration = TimeSpan.FromMinutes(2),
        };

        // Assert
        Assert.True(upgrade.IsFinished);
    }
    
    [Fact]
    public void IsNotFinished()
    {
        // Arrange
        BuildingUpgrade  upgrade = new BuildingUpgrade
        {
            Start = DateTime.Now,
            Duration = TimeSpan.FromMinutes(2),
        };

        // Assert
        Assert.False(upgrade.IsFinished);
    }
}