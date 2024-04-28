namespace SpaceBattles.Core.Application.Services;

using SpaceBattles.Core.Domain.Entities.Universe;

public sealed class StatisticService
{
    private readonly Dictionary<int, PlanetStatistics> _planetStatistics;

    public StatisticService()
    {
        _planetStatistics = new Dictionary<int, PlanetStatistics>();
    }

    public PlanetStatistics this[Planet planet]
    {
        get => _planetStatistics[planet.Id];
        set => _planetStatistics[planet.Id] = value;
    }

    public PlanetStatistics this[int id]
    {
        get => _planetStatistics[id];
        set => _planetStatistics[id] = value;
    }

    public void StartNewPlanetStatistics(Planet planet)
    {
        _planetStatistics.Add(planet.Id, new PlanetStatistics(planet.Id));
    }

    public IEnumerable<PlanetStatistics> GetValues()
        => _planetStatistics.Values;
}