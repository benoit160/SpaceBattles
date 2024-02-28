using SpaceBattles.Core.Domain.Entities.Building;

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
    
    public void UpdateUniverse()
    {
        foreach (var planet in _gameState.CurrentUniverse.Planets)
        {
            UpdatePlanet(planet);
        }
    }

    public void UpdateCurrentPlanet()
    {
        Span<long> totals = stackalloc long[3];
        PlanetStatistics stat = _statisticService[_gameState.CurrentPlanet];
        
        _gameState.CurrentPlanet.ResourcesUpdate(DateTime.Now, totals);
        BuildingLevel? result = _gameState.CurrentPlanet.ProcessUpgrades(DateTime.Now);

        if (result is not null)
        {
            _notificationService.NotifyInfo($"The following building upgrade is finished : {result.Building.Name}");
        }

        stat.TotalTitaniumProduced += totals[0];
        stat.TotalSiliconProduced += totals[1];
        stat.TotalHeliumProduced += totals[2];
    }

    private void UpdatePlanet(Domain.Entities.Universe.Planet planet)
    {
        Span<long> totals = stackalloc long[3];
        PlanetStatistics stat = _statisticService[planet];
        
        planet.ResourcesUpdate(DateTime.Now, totals);
        planet.ProcessUpgrades(DateTime.Now);

        stat.TotalTitaniumProduced += totals[0];
        stat.TotalSiliconProduced += totals[1];
        stat.TotalHeliumProduced += totals[2];
    }
}