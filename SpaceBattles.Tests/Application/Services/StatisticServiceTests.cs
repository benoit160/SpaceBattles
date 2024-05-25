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
        StatisticService service = new StatisticService();
        GameState state = new GameState(service);
        state.Initialize(model);


        // Act
        PlanetStatistics statistics = service[state.CurrentPlanet.Id];

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
        StatisticService service = new StatisticService();
        GameState state = new GameState(service);
        state.Initialize(model);
        
        PlanetStatistics statistics = service[state.CurrentPlanet.Id];
        DateTime timeBeforeUpdated = state.CurrentPlanet.LastUpdated;

        PlanetService planetService = new PlanetService(state, service, _notificationService);

        // Act
        planetService.UpdateCurrentPlanet(timeBeforeUpdated + TimeSpan.FromHours(1));

        // Assert
        Assert.Equal(30, statistics.TotalTitaniumProduced);
        Assert.Equal(15, statistics.TotalSiliconProduced);
        Assert.Equal(0, statistics.TotalHeliumProduced);
    }
    
    [Fact]
    public void ResourcesProduced_StillCounting_AfterMaximumCapacityReached()
    {
        // Arrange
        UniverseCreationModel model = new UniverseCreationModel();
        StatisticService service = new StatisticService();
        GameState state = new GameState(service);
        state.Initialize(model);
        
        PlanetStatistics statistics = service[state.CurrentPlanet.Id];
        DateTime timeBeforeUpdated = state.CurrentPlanet.LastUpdated;

        PlanetService planetService = new PlanetService(state, service, _notificationService);

        // Act
        planetService.UpdateCurrentPlanet(timeBeforeUpdated + TimeSpan.FromDays(100));

        // Assert
        Assert.True(statistics.TotalTitaniumProduced > 10_000);
        Assert.True(statistics.TotalSiliconProduced > 10_000);
        Assert.Equal(0, statistics.TotalHeliumProduced);
    }
}