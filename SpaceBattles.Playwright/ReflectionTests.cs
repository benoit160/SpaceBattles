namespace SpaceBattles.Playwright;

public class ReflectionTests : BlazorTest<WebApplicationFactory<Server.Program>, Server.Program>
{
    public static class ReflectionDataSource
    {
        public static IEnumerable<string> Routes()
        {
            List<string> routes = BlazorReflectionEngine<UI._Imports>
                .GetRoutes()
                .ToList();

            return routes;
        }
    }

    [Test]
    [MethodDataSource(typeof(ReflectionDataSource), nameof(ReflectionDataSource.Routes))]
    public async Task AllPagesFromReflection(string route)
    {
        // Act
        await Page.GotoAsync(new Uri(RootUri, route).AbsoluteUri);
    }

    [Test]
    public async Task NotFound()
    {
        // Arrange & Act
        await Page.GotoAsync(new Uri(RootUri, "potato").AbsoluteUri);

        // Assert
        await Expect(Page.Locator("h3")).ToHaveTextAsync("Not Found");
    }
}