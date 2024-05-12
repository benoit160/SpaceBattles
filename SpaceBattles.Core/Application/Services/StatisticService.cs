namespace SpaceBattles.Core.Application.Services;

public sealed class StatisticService
{
    private readonly Dictionary<int, PlanetStatistics> _planetStatistics = new();

    public PlanetStatistics this[int id]
    {
        get => _planetStatistics[id];
        set => _planetStatistics[id] = value;
    }
}