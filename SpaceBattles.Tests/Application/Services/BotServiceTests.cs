using SpaceBattles.Core.Application.Services;
using SpaceBattles.Core.Domain.Models;

namespace SpaceBattles.Tests.Application.Services;

public class BotServiceTests
{
    private readonly GameState _gameState;

    public BotServiceTests()
    {
        StatisticService statisticService = new StatisticService();
        _gameState = new GameState(statisticService);
    }
    
    [Fact]
    public void TryStartService_NoBots()
    {
        // Arrange
        UniverseCreationModel model = new UniverseCreationModel
        {
            IncludeBots = false,
        };
        _gameState.Initialize(model);
        BotService service = new BotService(_gameState);

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
        _gameState.Initialize(model);
        BotService service = new BotService(_gameState);

        // Act
        bool result = service.StartService();

        // Assert
        Assert.True(result);
    }
}