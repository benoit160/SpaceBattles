using SpaceBattles.Core.Domain.Entities.Building;
using SpaceBattles.Core.Domain.Entities.Universe;
using SpaceBattles.Core.Domain.Interfaces;

namespace SpaceBattles.UI.Tests.Components;

public class TotalCostDisplayTests : TestContext
{
    public TotalCostDisplayTests()
    {
        Services.AddMudServices();
        JSInterop.Mode = JSRuntimeMode.Loose;
    }
    
    [Fact]
    public void Component_Render()
    {
        // Arrange
        Planet planet = new Planet();
        planet.Init();
        
        IRequirements requirements = planet.Buildings[0];
        
        // Act
        IRenderedComponent<TotalCostDisplay> cut = RenderComponent<TotalCostDisplay>(parameters =>
        {
            parameters.Add(p => p.Planet, planet);
            parameters.Add(p => p.Upgrade, requirements);
        });

        // Assert
        IReadOnlyList<IRenderedComponent<ResourceDisplay>> list = cut.FindComponents<ResourceDisplay>();
        Assert.True(list.Count == 2);
    }
    
    [Theory]
    [InlineData(0, "~ 1 minute")]
    [InlineData(1, "~ 2 minutes")]
    [InlineData(11, "~ 2 hour")]
    public void Component_Render_DurationText(short level, string expectedText)
    {
        // Arrange
        Planet planet = new Planet();
        planet.Init();
        
        BuildingLevel requirements = planet.Buildings[0];
        requirements.Level = level;
        
        // Act
        IRenderedComponent<TotalCostDisplay> cut = RenderComponent<TotalCostDisplay>(parameters =>
        {
            parameters.Add(p => p.Planet, planet);
            parameters.Add(p => p.Upgrade, requirements);
        });

        // Assert
        Assert.Contains(expectedText, cut.Markup);
    }
    
    [Fact]
    public void Component_Render_IUpgrade_Is_Spaceship()
    {
        // Arrange
        Planet planet = new Planet();
        planet.Init();
        
        IRequirements requirements = planet.Spaceships.Span[0].CombatEntity;
        
        // Act
        IRenderedComponent<TotalCostDisplay> cut = RenderComponent<TotalCostDisplay>(parameters =>
        {
            parameters.Add(p => p.Planet, planet);
            parameters.Add(p => p.Upgrade, requirements);
        });

        // Assert
        IReadOnlyList<IRenderedComponent<ResourceDisplay>> list = cut.FindComponents<ResourceDisplay>();
        Assert.True(list.Count == 2);
    }
    
    [Fact]
    public void Component_Render_ThreeResourcesCosts()
    {
        // Arrange
        Planet planet = new Planet();
        planet.Init();
        
        IRequirements requirements = planet.Buildings.Last();
        
        // Act
        IRenderedComponent<TotalCostDisplay> cut = RenderComponent<TotalCostDisplay>(parameters =>
        {
            parameters.Add(p => p.Planet, planet);
            parameters.Add(p => p.Upgrade, requirements);
        });

        // Assert
        IReadOnlyList<IRenderedComponent<ResourceDisplay>> list = cut.FindComponents<ResourceDisplay>();
        Assert.True(list.Count == 3);
    }
}