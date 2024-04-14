namespace SpaceBattles.Core.Application.Services;

using SpaceBattles.Core.Domain.Entities.Universe;
using SpaceBattles.Core.Domain.Models;

public sealed class GameState
{
    public GameState()
    {
        CurrentPlanet = default!;
        CurrentUniverse = default!;
    }

    public Planet CurrentPlanet { get; private set; }

    public Universe CurrentUniverse { get; private set; }

    public void Initialize(UniverseCreationModel model)
    {
        CurrentUniverse = Universe.CreateUniverse(model);
        CurrentPlanet = CurrentUniverse.Planets.Single(planet => !planet.Owner?.IsBot ?? false);
    }

    public void Restore(Universe universe)
    {
        CurrentUniverse = universe;
        CurrentPlanet = CurrentUniverse.Planets.Single(planet => !planet.Owner?.IsBot ?? false);
    }
}