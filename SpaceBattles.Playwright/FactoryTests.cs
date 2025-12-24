using System.Net;

namespace SpaceBattles.Playwright;

public class FactoryTests : BlazorTest<WebApplicationFactory<Server.Program>, Server.Program>
{
    [Test]
    public async Task CreateFactory()
    {
        // Arrange
        WebApplicationFactory<Server.Program> factory = new();
        HttpClient client = factory.CreateClient();

        // Act
        HttpResponseMessage result = await client.GetAsync("/");

        // Assert
        await Assert.That(result.StatusCode).IsEqualTo(HttpStatusCode.OK);
        await Assert.That(result.Content.Headers).Contains(h => h.Key == "Content-Type" && h.Value.Contains("text/html"));
    }
}