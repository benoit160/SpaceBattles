namespace SpaceBattles.Core.Domain.GameSystems;

public class Process
{
    public Process(int workNeeded)
    {
        WorkNeeded = workNeeded;
        Start = default;
    }

    public DateTime Start { get; set; }

    public int WorkNeeded { get; init; }

    public int WorkDone { get; private set; }

    public int RemainingWork => WorkNeeded - WorkDone;

    public void AddWork(int quantity)
        => WorkDone += quantity;

    public TimeSpan EstimatedDuration(double production)
        => TimeSpan.FromSeconds(RemainingWork / production);

    public DateTime EstimatedEnd(double production)
        => Start + EstimatedDuration(production);

    public bool IsFinished => RemainingWork <= 0;

    public double PercentFinished => 100 * (WorkDone / (double)WorkNeeded);
}