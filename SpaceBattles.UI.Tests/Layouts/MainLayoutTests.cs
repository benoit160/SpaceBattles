using SpaceBattles.UI.Layouts;

namespace SpaceBattles.UI.Tests.Layouts;

public class MainLayoutTests : TestContext
{
    public MainLayoutTests()
    {
        Services.AddMudServices();
        JSInterop.SetupVoid("mudPopover.initialize", _ => true);
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