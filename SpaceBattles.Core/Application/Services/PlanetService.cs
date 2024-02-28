namespace SpaceBattles.Core.Application.Services;

public class PlanetService
{
    private readonly GameState _gameState;
    private readonly StatisticService _statisticService;

    public PlanetService(GameState gameState, StatisticService statisticService)
    {
        _gameState = gameState;
        _statisticService = statisticService;
    }

    public void UpdateCurrentPlanet()
    {
        Span<long> totals = stackalloc long[3];
        PlanetStatistics stat = _statisticService[_gameState.CurrentPlanet];
        
        _gameState.CurrentPlanet.ResourcesUpdate(DateTime.Now, totals);

        stat.TotalTitaniumProduced += totals[0];
        stat.TotalSiliconProduced += totals[1];
        stat.TotalHeliumProduced += totals[2];
    }
}