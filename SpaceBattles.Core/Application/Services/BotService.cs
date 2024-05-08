namespace SpaceBattles.Core.Application.Services;

using SpaceBattles.Core.Domain.Entities.Universe;

public sealed class BotService : IDisposable
{
    private readonly GameState _gameState;
    private readonly PeriodicTimer _timer = new(TimeSpan.FromSeconds(3));
    private CancellationTokenSource? _source;

    private IEnumerable<Planet> _botsPlanets;

    public BotService(GameState gameState)
    {
        _botsPlanets = Enumerable.Empty<Planet>();
        _gameState = gameState;
    }

    public bool StartService()
    {
        _botsPlanets = _gameState.CurrentUniverse.Planets
            .Where(planet => planet.Owner?.IsBot ?? false);

        _source = new CancellationTokenSource();

        // no bots
        if (!_botsPlanets.Any()) return false;

        Task.Run(async () =>
        {
            while (await _timer.WaitForNextTickAsync(_source.Token))
            {
                foreach (Planet planet in _botsPlanets)
                {
                    DoAction(planet);
                }
            }
        });

        return true;
    }

    public void Dispose()
    {
        _source?.Cancel();
        _source?.Dispose();
    }

    private static void DoAction(Planet planet)
    {
        planet.ProcessUpgrades(DateTime.Now);
        planet.ResourcesUpdate(DateTime.Now, stackalloc long[3]);

        Span<byte> indexes = stackalloc byte[12];
        for (byte i = 0; i < indexes.Length; i++)
            indexes[i] = (byte)(i + 1);
        Random.Shared.Shuffle(indexes);

        bool hasUpgraded = false;
        byte index = 0;

        while (!hasUpgraded && index < indexes.Length)
        {
            hasUpgraded |= planet.TryUpgradeBuilding(indexes[index]);
            index++;
        }
    }
}