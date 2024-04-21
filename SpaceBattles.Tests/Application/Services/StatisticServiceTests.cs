using Moq;
using SpaceBattles.Core.Application.Services;
using SpaceBattles.Core.Domain.Models;

namespace SpaceBattles.Tests.Application.Services;

public class StatisticServiceTests
{
    private readonly INotificationService _notificationService;

    public StatisticServiceTests()
    {
        _notificationService = new Mock<INotificationService>().Object;
    }
    
    [Fact]
    public void GetPlanetStatistics()
    {
        // Arrange
        UniverseCreationModel model = new UniverseCreationModel();
        GameState state = new GameState();
        state.Initialize(model);
        
        StatisticService service = new StatisticService(state);

        // Act
        PlanetStatistics statistics = service[state.CurrentPlanet];

        // Assert
        Assert.Equal(0, statistics.TotalTitaniumProduced);
        Assert.Equal(0, statistics.TotalSiliconProduced);
        Assert.Equal(0, statistics.TotalHeliumProduced);
    }
    
    [Fact]
    public void ResourcesProduced()
    {
        // Arrange
        UniverseCreationModel model = new UniverseCreationModel();
        GameState state = new GameState();
        state.Initialize(model);
        
        StatisticService service = new StatisticService(state);
        PlanetStatistics statistics = service[state.CurrentPlanet];
        DateTime timeBeforeUpdated = state.CurrentPlanet.LastUpdated;

        PlanetService planetService = new PlanetService(state, service, _notificationService);

        // Act
        planetService.UpdateCurrentPlanet(timeBeforeUpdated + TimeSpan.FromMinutes(10));

        // Assert
        Assert.Equal(300, statistics.TotalTitaniumProduced);
        Assert.Equal(150, statistics.TotalSiliconProduced);
        Assert.Equal(0, statistics.TotalHeliumProduced);
    }
    
    [Fact]
    public void ResourcesProduced_StillCounting_AfterMaximumCapacityReached()
    {
        // Arrange
        UniverseCreationModel model = new UniverseCreationModel();
        GameState state = new GameState();
        state.Initialize(model);
        
        StatisticService service = new StatisticService(state);
        PlanetStatistics statistics = service[state.CurrentPlanet];
        DateTime timeBeforeUpdated = state.CurrentPlanet.LastUpdated;

        PlanetService planetService = new PlanetService(state, service, _notificationService);

        // Act
        planetService.UpdateCurrentPlanet(timeBeforeUpdated + TimeSpan.FromDays(10));

        // Assert
        Assert.True(statistics.TotalTitaniumProduced > 10_000);
        Assert.True(statistics.TotalSiliconProduced > 10_000);
        Assert.Equal(0, statistics.TotalHeliumProduced);
    }
}