namespace SpaceBattles.Core.Domain.GameSystems;

public abstract class Process
{
    public DateTime Start { get; init; }

    public int WorkNeeded { get; init; }

    public int WorkDone { get; private set; }

    public int RemainingWork => WorkNeeded - WorkDone;

    public void Work(int quantity)
        => WorkDone += quantity;

    public TimeSpan EstimatedDuration(double production)
        => TimeSpan.FromSeconds(RemainingWork / production);

    public DateTime EstimatedEnd(double production)
        => Start + EstimatedDuration(production);

    public bool IsFinished => RemainingWork <= 0;

    public double PercentFinished => 100 * (WorkDone / (double)WorkNeeded);
}