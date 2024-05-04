using Bunit.TestDoubles;
using Microsoft.Extensions.DependencyInjection;
using SpaceBattles.Core.Application.Services;
using SpaceBattles.Core.Domain.Models;
using SpaceBattles.UI.Pages;
using SpaceBattles.UI.Services;

namespace SpaceBattles.UI.Tests.Pages;

public class BuildingTests : TestContext
{
    public BuildingTests()
    {
        Services.AddMudServices();
        Services.AddCoreSpaceBattlesServices();
        Services.AddScoped<INotificationService, NotificationService>();
        JSInterop.SetupVoid("mudPopover.initialize", "mudblazor-main-content", 0);
    }
    
    [Fact]
    public void Component_Render()
    {
        // Arrange
        var state = Services.GetRequiredService<GameState>();
        state.Initialize(new UniverseCreationModel());
        
        // Act
        RenderComponent<Building>(parameters =>
        {
            parameters.Add(p => p.BuildingName, "Titanium-Mine");
        });

        // Assert
        Assert.True(true);
    }
    
    [Fact]
    public void Invalid_Building_Name()
    {
        // Arrange
        var state = Services.GetRequiredService<GameState>();
        state.Initialize(new UniverseCreationModel());
        
        // Act
        RenderComponent<Building>(parameters =>
        {
            parameters.Add(p => p.BuildingName, "potato");
        });

        // Assert
        FakeNavigationManager navigationManager = Services.GetRequiredService<FakeNavigationManager>();
        Assert.Equal("http://localhost/Buildings", navigationManager.Uri);
    }
}