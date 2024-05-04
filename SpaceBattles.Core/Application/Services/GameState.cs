namespace SpaceBattles.Core.Application.Services;

using SpaceBattles.Core.Domain.Entities.Universe;
using SpaceBattles.Core.Domain.Models;

public class GameState
{
    private readonly StatisticService _statistic;

    public GameState(StatisticService statistic)
    {
        _statistic = statistic;
        CurrentPlanet = default!;
        CurrentUniverse = default!;
    }

    public Planet CurrentPlanet { get; private set; }

    public Universe CurrentUniverse { get; private set; }

    public void SetState(Universe universe)
    {
        CurrentUniverse = universe;
        CurrentPlanet = CurrentUniverse.Planets.Single(planet => !planet.Owner?.IsBot ?? false);
    }

    public void Initialize(UniverseCreationModel model)
    {
        CurrentUniverse = Universe.CreateUniverse(model);
        CurrentPlanet = CurrentUniverse.Planets.Single(planet => !planet.Owner?.IsBot ?? false);
        _statistic[CurrentPlanet.Id] = CurrentUniverse.Statistics;
    }
}