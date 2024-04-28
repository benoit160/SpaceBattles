namespace SpaceBattles.Core.Application.Services;

using SpaceBattles.Core.Domain.Entities.Player;
using SpaceBattles.Core.Domain.Entities.Universe;

public sealed class StatisticService
{
    private readonly Dictionary<int, PlanetStatistics> _planetStatistics;
    private readonly Dictionary<short, PlayerStatistics> _playerStatistics;

    public StatisticService(GameState gameState)
    {
        _planetStatistics = gameState.CurrentUniverse.Planets
            .Select(planet => (Planet: planet, stats: new PlanetStatistics(planet.Id)))
            .ToDictionary(tuple => tuple.Planet.Id, tuple => tuple.stats);

        _playerStatistics = gameState.CurrentUniverse.Players
            .Select(player => (Player: player, stats: new PlayerStatistics(player.Id)))
            .ToDictionary(tuple => tuple.Player.Id, tuple => tuple.stats);
    }

    public PlanetStatistics this[Planet planet]
    {
        get => _planetStatistics[planet.Id];
        set => _planetStatistics[planet.Id] = value;
    }

    public PlayerStatistics this[Player player]
    {
        get => _playerStatistics[player.Id];
        set => _playerStatistics[player.Id] = value;
    }

    public void StartNewPlanetStatistics(Planet planet)
    {
        _planetStatistics.Add(planet.Id, new PlanetStatistics(planet.Id));
    }

    public void StartNewPlayerStatistics(Player player)
    {
        _playerStatistics.Add(player.Id, new PlayerStatistics(player.Id));
    }
}