using AngleSharp.Dom;
using SpaceBattles.Core.Domain.Enums;
using SpaceBattles.Core.Domain.Records;

namespace SpaceBattles.UI.Tests.Components;

public class ResourceDisplayTests : TestContext
{
    public ResourceDisplayTests()
    {
        Services.AddMudServices();
        JSInterop.SetupVoid("mudPopover.initialize", "mudblazor-main-content", 0);
    }
    
    [Fact]
    public void Component_Render()
    {
        // Arrange
        ResourceCost cost = new ResourceCost(Resource.Titanium, 150);
        long currentQuantity = 175;
        
        // Act
        IRenderedComponent<ResourceDisplay> cut = RenderComponent<ResourceDisplay>(parameters =>
        {
            parameters.Add(p => p.Cost, cost);
            parameters.Add(p => p.Multiplier, 1);
            parameters.Add(p => p.CurrentQuantity,  currentQuantity);
        });

        // Assert
        Assert.Contains($"175 / 150", cut.Markup);
        Assert.Contains("mud-success-text", cut.Markup);
    }
    
    [Theory]
    [InlineData(1, "175 / 150", "mud-success-text")]
    [InlineData(2, "175 / 300", "mud-error-text")]
    public void Multiplier(int multiplier, string expectedMarkup, string expectedColor)
    {
        // Arrange
        ResourceCost cost = new ResourceCost(Resource.Titanium, 150);
        long currentQuantity = 175;
        
        // Act
        IRenderedComponent<ResourceDisplay> cut = RenderComponent<ResourceDisplay>(parameters =>
        {
            parameters.Add(p => p.Cost, cost);
            parameters.Add(p => p.Multiplier, multiplier);
            parameters.Add(p => p.CurrentQuantity,  currentQuantity);
        });

        // Assert
        Assert.Contains(expectedMarkup, cut.Markup);
        Assert.Contains(expectedColor, cut.Markup);
    }
    
    [Fact]
    public void Component_Render_RerenderWithEnoughQuantity()
    {
        // Arrange
        ResourceCost cost = new ResourceCost(Resource.Titanium, 150);
        long currentQuantity = 100;
        
        // Act
        IRenderedComponent<ResourceDisplay> cut = RenderComponent<ResourceDisplay>(parameters =>
        {
            parameters.Add(p => p.Cost, cost);
            parameters.Add(p => p.Multiplier, 1);
            parameters.Add(p => p.CurrentQuantity,  currentQuantity);
        });
        
        cut.SetParametersAndRender(parameters => parameters
            .Add(p => p.CurrentQuantity, 200)
        );

        // Assert
        Assert.Contains($"200 / 150", cut.Markup);
        Assert.Contains("mud-success-text", cut.Markup);
    }
    
    [Fact]
    public void Component_Render_InsufficientQuantity()
    {
        // Arrange
        ResourceCost cost = new ResourceCost(Resource.Titanium, 150);
        long currentQuantity = 100;
        
        // Act
        IRenderedComponent<ResourceDisplay> cut = RenderComponent<ResourceDisplay>(parameters =>
        {
            parameters.Add(p => p.Cost, cost);
            parameters.Add(p => p.Multiplier, 1);
            parameters.Add(p => p.CurrentQuantity,  currentQuantity);
        });

        // Assert
        Assert.Contains($"100 / 150", cut.Markup);
        Assert.Contains("mud-error-text", cut.Markup);
    }

    [Theory]
    [InlineData(Resource.Titanium, "/images/items/titanium.avif")]
    [InlineData(Resource.Silicon, "/images/items/silicon.avif")]
    [InlineData(Resource.Helium, "/images/items/helium.avif")]
    public void Component_Render_ResourcesImages(Resource resource, string expectedSrc)
    {
        // Arrange
        ResourceCost cost = new ResourceCost(resource, 150);
        long currentQuantity = 175;

        // Act
        IRenderedComponent<ResourceDisplay> cut = RenderComponent<ResourceDisplay>(parameters =>
        {
            parameters.Add(p => p.Cost, cost);
            parameters.Add(p => p.CurrentQuantity, currentQuantity);
        });

        // Assert
        IElement img = cut.Find("img");
        Assert.True(img.GetAttribute("src") == expectedSrc);
    }
}