using SpaceBattles.Core.Domain.Entities.Universe;
using SpaceBattles.Core.Domain.Models;

namespace SpaceBattles.Core.Application.Services;

public sealed class GameState
{
    public Planet CurrentPlanet { get; private set; }
    
    public Universe CurrentUniverse { get; private set; }

    public GameState()
    {
        CurrentPlanet = default!;
        CurrentUniverse = default!;
    }

    public void Initialize()
    {
        UniverseCreationModel model = new UniverseCreationModel
        {
            NumberOfPlanets = 10,
            IncludeBots = true,
            IsPeacefulMode = true,
            UniverseSpeed = 1f,
            UniverseName = "Ben"
        };
        
        CurrentUniverse = Universe.CreateUniverse(model);
        CurrentPlanet = CurrentUniverse.Planets.First();
    }
}