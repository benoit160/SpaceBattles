using SpaceBattles.Core.Application.Services;
using SpaceBattles.Core.Domain.Models;

namespace SpaceBattles.Tests.Application.Services;

public class StatisticServiceTests
{
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
}