using SpaceBattles.Core.Domain.Entities.Universe;

namespace SpaceBattles.Core.Application.Services;

public sealed class GameState
{
    public Planet CurrentPlanet { get; private set; }

    public GameState()
    {
        CurrentPlanet = new Planet();
    }
}