using SpaceBattles.Core.Domain.Entities.Building;
using SpaceBattles.Core.Domain.Entities.Universe;

namespace SpaceBattles.Core.Application.Services;

public sealed class PlanetService
{
    private readonly GameState _gameState;
    private readonly StatisticService _statisticService;
    private readonly INotificationService _notificationService;

    public PlanetService(GameState gameState, StatisticService statisticService, INotificationService notificationService)
    {
        _gameState = gameState;
        _statisticService = statisticService;
        _notificationService = notificationService;
    }
    
    public void UpdateCurrentPlanet()
    {
        Span<long> totals = stackalloc long[3];
        PlanetStatistics stat = _statisticService[_gameState.CurrentPlanet];
        Planet planet = _gameState.CurrentPlanet;
        BuildingLevel? result = null;
        
        if (planet.BuildingUpgrade is null)
        {
            planet.ResourcesUpdate(DateTime.Now, totals);
        }
        else
        {
            planet.ResourcesUpdate(planet.BuildingUpgrade.End, totals);
            result = planet.ProcessUpgrades(DateTime.Now);
            planet.ResourcesUpdate(DateTime.Now, totals);
        }

        if (result is not null)
        {
            _notificationService.NotifyInfo($"The building upgrade is finished : {result.Building.Name}");
        }

        stat.TotalTitaniumProduced += totals[0];
        stat.TotalSiliconProduced += totals[1];
        stat.TotalHeliumProduced += totals[2];
    }
}