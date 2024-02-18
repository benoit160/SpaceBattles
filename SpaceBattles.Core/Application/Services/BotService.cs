using SpaceBattles.Core.Application.Helpers;
using SpaceBattles.Core.Domain.Entities.Building;
using SpaceBattles.Core.Domain.Entities.Universe;

namespace SpaceBattles.Core.Application.Services;

public sealed class BotService : IAsyncDisposable
{
    private readonly GameState _gameState;
    private readonly PeriodicTimer _timer = new(TimeSpan.FromSeconds(20));
    private readonly CancellationTokenSource _source = new();
    private readonly List<Planet> _planets = new();

    public BotService(GameState gameState)
    {
        _gameState = gameState;
    }

    public void StartService()
    {
        _planets.AddRange(_gameState.CurrentUniverse.Planets.Where(x => x.IsComputer));
        
        // no bots
        if (_planets.Count == 0) return;

        BlazorDebug.WriteLine("Bot service started");
        var a = Task.Run(async () =>
        {
            while (await _timer.WaitForNextTickAsync(_source.Token))
            {
                BlazorDebug.WriteLine("Updating bots");
                foreach (Planet planet in _planets)
                {
                    DoAction(planet);
                }
            }
        });
    }

    public void DoAction(Planet planet)
    {
        planet.ProcessUpgrades(DateTime.Now);
        planet.ResourcesUpdate(DateTime.Now);

        planet.TryUpgradeBuilding(5);
        planet.TryUpgradeBuilding(3);
        planet.TryUpgradeBuilding(1);
    }
    
    public async ValueTask DisposeAsync()
    {
        BlazorDebug.WriteLine("Bot service disposing");
        await _source.CancelAsync();
        _source.Dispose();
    }
}