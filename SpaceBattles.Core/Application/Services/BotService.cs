namespace SpaceBattles.Core.Application.Services;

using SpaceBattles.Core.Domain.Entities.Universe;

public sealed class BotService : IDisposable
{
    private readonly ITimeProvider _timeProvider;
    private readonly GameState _gameState;
    private readonly CancellationTokenSource _source = new();

    private IEnumerable<Planet> _botsPlanets;

    public BotService(GameState gameState, ITimeProvider timeProvider)
    {
        _botsPlanets = Enumerable.Empty<Planet>();
        _gameState = gameState;
        _timeProvider = timeProvider;
    }

    public bool StartService()
    {
        _botsPlanets = _gameState.CurrentUniverse.Planets
            .Where(planet => planet.Owner?.IsBot ?? false);

        // no bots
        if (!_botsPlanets.Any()) return false;

        Task.Run(async () =>
        {
            while (await _timeProvider.WaitForNextTickAsync(_source.Token))
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
        _source.Cancel();
        _source.Dispose();
    }

    private void DoAction(Planet planet)
    {
        planet.ProcessUpgrades(_timeProvider.Now);
        planet.ResourcesUpdate(_timeProvider.Now, stackalloc long[3]);

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