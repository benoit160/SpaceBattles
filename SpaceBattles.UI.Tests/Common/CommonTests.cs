using System.Reflection;
using Microsoft.AspNetCore.Components;

namespace SpaceBattles.UI.Tests.Common;

public class CommonTests
{
    private readonly Assembly _uiLibrary;
    
    public CommonTests()
    {
        _uiLibrary = typeof(_Imports).Assembly;
    }
    
    [Fact]
    public void NoDuplicateRoutes()
    {
        // Arrange
        IEnumerable<Type> pages = BlazorAssembly.GetBlazorPages(_uiLibrary);
        IEnumerable<RouteAttribute> routes = pages.SelectMany(page => page.GetCustomAttributes<RouteAttribute>());
        IEnumerable<string> templates = routes.Select(route => route.Template);
        
        // Act
        HashSet<string> uniqueTemplates = templates.ToHashSet();

        // Assert
        Assert.Equal(uniqueTemplates.Count, templates.Count());
    }
}