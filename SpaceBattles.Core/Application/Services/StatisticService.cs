namespace SpaceBattles.Core.Application.Services;

using SpaceBattles.Core.Domain.Entities.Universe;

public sealed class StatisticService
{
    private readonly Dictionary<int, PlanetStatistics> _planetStatistics;

    public StatisticService()
    {
        _planetStatistics = new Dictionary<int, PlanetStatistics>();
    }

    public PlanetStatistics this[int id]
    {
        get => _planetStatistics[id];
        set => _planetStatistics[id] = value;
    }
}