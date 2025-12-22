namespace SpaceBattles.Core.Application.Services;

public interface ITimeProvider
{
    public DateTime Now { get; }

    public ValueTask<bool> WaitForNextTickAsync(CancellationToken cancellationToken);
}