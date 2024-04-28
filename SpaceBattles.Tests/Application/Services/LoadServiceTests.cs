using System.Net;
using Moq;
using Moq.Protected;
using SpaceBattles.Core.Application.Services;

namespace SpaceBattles.Tests.Application.Services;

public class LoadServiceTests
{
    private readonly Mock<HttpMessageHandler> _msgHandler;

    public LoadServiceTests()
    {
        _msgHandler = new Mock<HttpMessageHandler>();
    }

    [Fact]
    public async Task GetManifest()
    {
        // Arrange
        using StreamReader reader = new StreamReader("./TestFiles/manifest.txt");

        string fileContent = await reader.ReadToEndAsync();
        
        _msgHandler.Protected().Setup<Task<HttpResponseMessage>>(
            "SendAsync", 
            ItExpr.IsAny<HttpRequestMessage>(), 
            ItExpr.IsAny<CancellationToken>()
        ).ReturnsAsync(new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.OK,
            Content = new StringContent(fileContent),
        });
        
        HttpClient client = new HttpClient(_msgHandler.Object)
        {
            BaseAddress = new Uri("http://www.spacebattles.azurewebsites.net"),
        };
        LoadService service = new LoadService(client);
        
        // Act
        LoadService.Manifest? manifest = await service.GetAndReadManifest();

        // Assert
        Assert.NotNull(manifest);
    }
}