using SpaceBattles.Core.Application.Services;

namespace SpaceBattles.Tests;

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