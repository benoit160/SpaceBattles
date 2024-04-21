namespace SpaceBattles.Core.Application.Services;

using SpaceBattles.Core.Domain.Entities.Universe;

public interface ITimeProvider
{
    public DateTime Now { get; }

    public ValueTask<bool> WaitForNextTickAsync(CancellationToken cancellationToken);
}

public sealed class TimeProvider : ITimeProvider
{
    private readonly PeriodicTimer _timer;

    public TimeProvider(int secondsInterval)
    {
        _timer = new(TimeSpan.FromSeconds(secondsInterval));
    }

    public DateTime Now => DateTime.Now;

    public ValueTask<bool> WaitForNextTickAsync(CancellationToken cancellationToken)
        => _timer.WaitForNextTickAsync(cancellationToken);
}

public sealed class TestTimeProvider : ITimeProvider
{
    private readonly int _secondIncrement;
    private DateTime _datetime;
    private int _cyclesLeft;

    public TestTimeProvider(int secondsInterval, int cyclesToSimulate)
    {
        _secondIncrement = secondsInterval;
        _cyclesLeft = cyclesToSimulate;
        _datetime = DateTime.Now;
    }

    public DateTime Now
    {
        get
        {
            _datetime += TimeSpan.FromSeconds(_secondIncrement);
            return _datetime;
        }
    }

    public bool IsFinished => _cyclesLeft == 0;

    public ValueTask<bool> WaitForNextTickAsync(CancellationToken cancellationToken)
        => ValueTask.FromResult(_cyclesLeft-- > 0);
}

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