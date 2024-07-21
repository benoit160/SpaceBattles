namespace SpaceBattles.UI.Tests.Components;

public class UniverseCreationWaiterTests  : TestContext
{
    public UniverseCreationWaiterTests()
    {
        Services.AddMudServices();
    }
    
    [Fact]
    public void Components_Render()
    {
        // Arrange
        
        // Act
        IRenderedComponent<UniverseCreationWaiter> cut = RenderComponent<UniverseCreationWaiter>();
        
        // Assert
        Assert.Contains("...", cut.Markup);
    }
    
    [Theory]
    [InlineData(true, "Creating universe...")]
    [InlineData(false, "Loading your save data...")]
    public async Task ShowWaitingScreen(bool creation, string expectedMarkup)
    {
        // Arrange
        
        // Act
        IRenderedComponent<UniverseCreationWaiter> cut = RenderComponent<UniverseCreationWaiter>();

        await cut.InvokeAsync(() => cut.Instance.ShowWaitingScreen(creation));
        
        // Assert
        Assert.Equal(2, cut.RenderCount);
        Assert.Contains(expectedMarkup, cut.Markup);
    }
}