namespace SpaceBattles.Core.Domain.Entities.Upgrade;

public abstract class Process
{
    public DateTime Start { get; init; }

    public int WorkNeeded { get; init; }

    public int WorkDone { get; set; }

    public int RemainingWork => WorkNeeded - WorkDone;

    public TimeSpan EstimatedDuration(double production)
        => TimeSpan.FromSeconds(RemainingWork / production);

    public DateTime EstimatedEnd(double production)
        => Start + EstimatedDuration(production);

    public bool IsFinished => RemainingWork <= 0;

    public double PercentFinished => 100 * (WorkDone / (double)WorkNeeded);
}