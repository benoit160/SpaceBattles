namespace SpaceBattles.Core.Application.Services;

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