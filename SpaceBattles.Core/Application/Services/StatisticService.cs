namespace SpaceBattles.Core.Application.Services;

using SpaceBattles.Core.Domain.Entities.Universe;

public sealed class StatisticService
{
    private readonly Dictionary<int, PlanetStatistics> _planetStatistics;

    public StatisticService(GameState gameState)
    {
        _planetStatistics = gameState.CurrentUniverse.Planets
            .Select(planet => (Planet: planet, stats: new PlanetStatistics(planet.Id)))
            .ToDictionary(tuple => tuple.Planet.Id, tuple => tuple.stats);
    }

    public PlanetStatistics this[Planet planet]
    {
        get => _planetStatistics[planet.Id];
        set => _planetStatistics[planet.Id] = value;
    }

    public void StartNewPlanetStatistics(Planet planet)
    {
        _planetStatistics.Add(planet.Id, new PlanetStatistics(planet.Id));
    }

    public IEnumerable<PlanetStatistics> GetValues()
        => _planetStatistics.Values;
}