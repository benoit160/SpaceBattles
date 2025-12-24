using System.Net;

namespace SpaceBattles.Playwright;

public class FactoryTests : BlazorTest
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
    }
}