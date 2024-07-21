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
        Assert.Contains("Creating universe..", cut.Markup);
    }
    
    [Fact]
    public async Task ShowWaitingScreen()
    {
        // Arrange
        
        // Act
        IRenderedComponent<UniverseCreationWaiter> cut = RenderComponent<UniverseCreationWaiter>();

        await cut.InvokeAsync(() => cut.Instance.ShowWaitingScreen(true));
        
        // Assert
        Assert.Equal(2, cut.RenderCount);
        Assert.Contains("Choosing the best starting planet for you..", cut.Markup);
    }
}