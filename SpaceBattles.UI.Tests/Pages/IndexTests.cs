using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using SpaceBattles.Core.Application.Services;
using Index = SpaceBattles.UI.Pages.Index;

namespace SpaceBattles.UI.Tests.Pages;

public class IndexTests : TestContext
{
    public IndexTests()
    {
        Services.AddMudServices();
        JSInterop.SetupVoid("mudPopover.initialize", _ => true);

        Services.AddSingleton<IBrowserService, BrowserService>();
        Services.AddSingleton<StatisticService>();
        Services.AddSingleton<GameState>();
        Services.AddSingleton<SaveService>();
    }
    
    [Fact]
    public void Component_Render()
    {
        // Arrange
        
        // Act
        IRenderedComponent<Index> cut = RenderComponent<Index>();

        // Assert
    } 
}