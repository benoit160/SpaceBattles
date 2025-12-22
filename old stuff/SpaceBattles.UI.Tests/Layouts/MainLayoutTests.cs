using SpaceBattles.UI.Layouts;

namespace SpaceBattles.UI.Tests.Layouts;

public class MainLayoutTests : TestContext
{
    public MainLayoutTests()
    {
        Services.AddMudServices();
        JSInterop.Mode = JSRuntimeMode.Loose;
    }
    
    [Fact]
    public void Components_Render()
    {
        // Arrange
        
        // Act
        RenderComponent<MainLayout>();
        
        // Assert
        Assert.True(true);
    }
}