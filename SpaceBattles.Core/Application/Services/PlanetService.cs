using System.Collections.Frozen;
using SpaceBattles.Core.Domain.Entities.Universe;

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
        _gameState.CurrentPlanet.ResourcesUpdate(DateTime.Now, totals);
        var stat = _statisticService[_gameState.CurrentPlanet];

        stat.TotalTitaniumProduced += totals[0];
        stat.TotalTitaniumProduced += totals[1];
        stat.TotalTitaniumProduced += totals[2];
    }
}

public class StatisticService
{
    private readonly FrozenDictionary<Planet, PlanetStatistics> _statistics;
    private readonly GameState _gameState;

    public StatisticService(GameState gameState)
    {
        _gameState = gameState;
        _statistics = gameState.CurrentUniverse.Planets
            .Select(planet => (Planet: planet, stats: new PlanetStatistics()))
            .ToFrozenDictionary(tuple => tuple.Planet, tuple => tuple.stats);
    }

    public PlanetStatistics this[Planet p]
    {
        get => _statistics[p];
    }
}

public sealed class PlanetStatistics
{
    public long TotalTitaniumProduced { get; set; }
    
    public long TotalSiliconProduced { get; set; }
    
    public long TotalHeliumProduced { get; set; }

    public int NumberOfAttackLaunched { get; set; }
    
    public int NumberOfAttackReceived { get; set; }
}