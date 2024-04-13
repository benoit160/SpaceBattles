namespace SpaceBattles.Core.Application.Services;

using System.Collections.Frozen;
using SpaceBattles.Core.Domain.Entities.Universe;

public sealed class StatisticService
{
    private readonly FrozenDictionary<Planet, PlanetStatistics> _statistics;

    public StatisticService(GameState gameState)
    {
        _statistics = gameState.CurrentUniverse.Planets
            .Select(planet => (Planet: planet, stats: new PlanetStatistics()))
            .ToFrozenDictionary(tuple => tuple.Planet, tuple => tuple.stats);
    }

    public PlanetStatistics this[Planet p]
    {
        get => _statistics[p];
    }
}