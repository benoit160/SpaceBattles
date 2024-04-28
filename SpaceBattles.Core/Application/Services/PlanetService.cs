namespace SpaceBattles.Core.Application.Services;

using SpaceBattles.Core.Domain.Entities.Building;
using SpaceBattles.Core.Domain.Entities.Universe;

public sealed class PlanetService
{
    private readonly GameState _gameState;
    private readonly StatisticService _statisticService;
    private readonly INotificationService _notificationService;

    public PlanetService(
        GameState gameState,
        StatisticService statisticService,
        INotificationService notificationService)
    {
        _gameState = gameState;
        _statisticService = statisticService;
        _notificationService = notificationService;
    }

    public void UpdateCurrentPlanet(DateTime now)
    {
        Span<long> totals = stackalloc long[3];
        PlanetStatistics stat = _statisticService[_gameState.CurrentPlanet.Id];
        Planet planet = _gameState.CurrentPlanet;
        BuildingLevel? result = null;

        if (planet.BuildingUpgrade is null)
        {
            planet.ResourcesUpdate(now, totals);
        }
        else
        {
            planet.ResourcesUpdate(planet.BuildingUpgrade.End, totals);
            result = planet.ProcessUpgrades(now);
            planet.ResourcesUpdate(now, totals);
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