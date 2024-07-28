using SpaceBattles.Core.Domain.GameSystems;

namespace SpaceBattles.Tests.Domain.GameSystems;

public class ProcessTests
{
    [Fact]
    public void Creation()
    {
        // Arrange
        Process process = new Process(100);

        // Act

        // Assert
        Assert.Equal(100, process.RemainingWork);
        Assert.Equal(TimeSpan.FromSeconds(100), process.EstimatedDuration(1d));
    }
    
    [Fact]
    public void Work()
    {
        // Arrange
        Process process = new Process(100);

        // Act
        process.AddWork(30);
        
        // Assert
        Assert.Equal(70, process.RemainingWork);
        Assert.Equal(TimeSpan.FromSeconds(70), process.EstimatedDuration(1d));
        Assert.False(process.IsFinished);
        Assert.Equal(30d, process.PercentFinished);
    }
    
    [Fact]
    public void Work_Complete()
    {
        // Arrange
        Process process = new Process(100);

        // Act
        process.AddWork(100);
        
        // Assert
        Assert.Equal(0, process.RemainingWork);
        Assert.Equal(TimeSpan.Zero, process.EstimatedDuration(1d));
        Assert.True(process.IsFinished);
        Assert.Equal(100d, process.PercentFinished);
    }
}