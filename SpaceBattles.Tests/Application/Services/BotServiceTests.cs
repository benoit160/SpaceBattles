using SpaceBattles.Core.Application.Services;
using SpaceBattles.Core.Domain.Models;

namespace SpaceBattles.Tests.Application.Services;

public class BotServiceTests
{
    [Fact]
    public void TryStartService_NoBots()
    {
        // Arrange
        UniverseCreationModel model = new UniverseCreationModel
        {
            IncludeBots = false,
        };
        GameState gameState = new GameState();
        gameState.Initialize(model);
        BotService service = new BotService(gameState);

        // Act
        bool result = service.StartService();

        // Assert
        Assert.False(result);
    }
    
    [Fact]
    public void TryStartService()
    {
        // Arrange
        UniverseCreationModel model = new UniverseCreationModel
        {
            IncludeBots = true,
        };
        GameState gameState = new GameState();
        gameState.Initialize(model);
        BotService service = new BotService(gameState);

        // Act
        bool result = service.StartService();

        // Assert
        Assert.True(result);
    }
}