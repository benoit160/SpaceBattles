using Moq;
using SpaceBattles.Core.Application.Services;
using SpaceBattles.Core.Domain.Models;

namespace SpaceBattles.Tests.Application.Services;

public class BotServiceTests
{
    private readonly TestTimeProvider _timeProvider;

    public BotServiceTests()
    {
        _timeProvider = new TestTimeProvider(60, 100000);
    }

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
        BotService service = new BotService(gameState, _timeProvider);

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
        BotService service = new BotService(gameState, _timeProvider);

        // Act
        bool result = service.StartService();

        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public void Action_Happenned_On_Planet()
    {
        // Arrange
        UniverseCreationModel model = new UniverseCreationModel
        {
            IncludeBots = true,
        };
        GameState gameState = new GameState();
        gameState.Initialize(model);
        BotService service = new BotService(gameState, _timeProvider);

        // Act
        service.StartService();

        while (!_timeProvider.IsFinished)
        {
        }

        // Assert
        Assert.Contains(gameState.CurrentUniverse.Planets, p => p.Buildings.Any(b => b.Level > 10));
    }
}