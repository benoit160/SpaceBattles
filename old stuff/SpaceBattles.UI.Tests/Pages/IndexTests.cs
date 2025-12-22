using Bunit.TestDoubles;
using Microsoft.Extensions.DependencyInjection;
using Index = SpaceBattles.UI.Pages.Index;

namespace SpaceBattles.UI.Tests.Pages;

public class IndexTests : TestContext
{
    public IndexTests()
    {
        Services.AddMudServices();
        Services.AddCoreSpaceBattlesServices();
        
        JSInterop.Setup<string?>("localStorage.getItem", "telemetry-ping")
            .SetResult(DateTime.Now.ToLongDateString());

        JSInterop.Setup<string>("localStorage.setItem", _ => true);
    }
    
    [Fact]
    public void Component_Render()
    {
        // Arrange

        // Act
        RenderComponent<Index>();

        // Assert
        Assert.True(true);
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("null")]
    public async Task StartGame_EmptyOrInvalidSaveData(string? getItemResult)
    {
        // Arrange
        JSInterop.Mode = JSRuntimeMode.Strict;

        JSInterop.Setup<string?>("localStorage.getItem", "SaveData")
            .SetResult(getItemResult);
        
        // Act
        IRenderedComponent<Index> cut = RenderComponent<Index>();
        cut.Find("button").Click();
        
        // Assert
        Assert.Contains(JSInterop.Invocations, js => js is
        {
            Identifier: "localStorage.getItem",
            Arguments: ["SaveData"],
        });
        
        await Task.Delay(3100);
        
        FakeNavigationManager navigationManager = Services.GetRequiredService<FakeNavigationManager>();
        Assert.Equal("http://localhost/Overview", navigationManager.Uri);
    }
}