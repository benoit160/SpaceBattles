namespace SpaceBattles.Core.Application.Services;

using SpaceBattles.Core.Domain.Entities.Player;
using SpaceBattles.Core.Domain.Entities.Universe;

public sealed class StatisticService
{
    private readonly Dictionary<Planet, PlanetStatistics> _planetStatistics;
    private readonly Dictionary<Player, PlayerStatistics> _playerStatistics;

    public StatisticService(GameState gameState)
    {
        _planetStatistics = gameState.CurrentUniverse.Planets
            .Select(planet => (Planet: planet, stats: new PlanetStatistics(planet.Id)))
            .ToDictionary(tuple => tuple.Planet, tuple => tuple.stats);

        _playerStatistics = gameState.CurrentUniverse.Players
            .Select(player => (Player: player, stats: new PlayerStatistics(player.Id)))
            .ToDictionary(tuple => tuple.Player, tuple => tuple.stats);
    }

    public PlanetStatistics this[Planet planet]
    {
        get => _planetStatistics[planet];
        set => _planetStatistics[planet] = value;
    }

    public PlayerStatistics this[Player player]
    {
        get => _playerStatistics[player];
        set => _playerStatistics[player] = value;
    }

    public void StartNewPlanetStatistics(Planet planet)
    {
        _planetStatistics.Add(planet, new PlanetStatistics(planet.Id));
    }

    public void StartNewPlayerStatistics(Player player)
    {
        _playerStatistics.Add(player, new PlayerStatistics(player.Id));
    }
}