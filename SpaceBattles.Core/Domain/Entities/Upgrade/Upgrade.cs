namespace SpaceBattles.Core.Domain.Entities.Upgrade;

public abstract class Upgrade
{
    public DateTime Start { get; set; }

    public TimeSpan Duration { get; set; }
    
    public DateTime End => Start + Duration;

    public TimeSpan RemainingTime => End - DateTime.Now;

    public bool IsStarted => DateTime.Now >= Start;

    public bool IsFinished => DateTime.Now >= End;
    
    public double EllapsedSeconds => (DateTime.Now - Start).TotalSeconds;
    
    public double PercentFinished => 100 * EllapsedSeconds / Duration.TotalSeconds;
}