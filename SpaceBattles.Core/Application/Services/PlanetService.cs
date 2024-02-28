namespace SpaceBattles.Core.Application.Services;

public sealed class PlanetService
{
    private readonly GameState _gameState;
    private readonly StatisticService _statisticService;

    public PlanetService(GameState gameState, StatisticService statisticService)
    {
        _gameState = gameState;
        _statisticService = statisticService;
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
        _gameState.CurrentPlanet.ProcessUpgrades(DateTime.Now);

        stat.TotalTitaniumProduced += totals[0];
        stat.TotalSiliconProduced += totals[1];
        stat.TotalHeliumProduced += totals[2];
    }

    private void UpdatePlanet(Domain.Entities.Universe.Planet planet)
    {
        Span<long> totals = stackalloc long[3];
        PlanetStatistics stat = _statisticService[planet];
        
        planet.ResourcesUpdate(DateTime.Now, totals);

        stat.TotalTitaniumProduced += totals[0];
        stat.TotalSiliconProduced += totals[1];
        stat.TotalHeliumProduced += totals[2];
    }
}