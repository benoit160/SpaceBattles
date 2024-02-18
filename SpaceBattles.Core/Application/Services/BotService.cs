using System.Diagnostics;
using SpaceBattles.Core.Domain.Entities.Universe;

namespace SpaceBattles.Core.Application.Services;

public sealed class BotService : IAsyncDisposable
{
    private readonly GameState _gameState;
    private readonly PeriodicTimer _timer = new(TimeSpan.FromSeconds(10));
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

        Debug.WriteLine("bot service started");
        _ = Task.Run(async () =>
        {
            while (await _timer.WaitForNextTickAsync(_source.Token))
            {
                Debug.WriteLine("Updating bots");
                foreach (Planet planet in _planets)
                {
                    DoAction(planet);
                }
            }
        });
    }

    public void DoAction(Planet planet)
    {
        // can't do anything
        if(planet.BuildingUpgrade is null) return;

        Span<short> updatableBuildingIds = stackalloc short[10];
    }

    public async ValueTask DisposeAsync()
    {
        Debug.WriteLine("bot service disposing");
        await _source.CancelAsync();
        _source.Dispose();
    }
}