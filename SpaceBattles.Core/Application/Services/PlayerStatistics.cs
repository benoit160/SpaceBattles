namespace SpaceBattles.Core.Application.Services;

public sealed class PlayerStatistics
{
    public PlayerStatistics(int playerId)
    {
        PlayerId = playerId;
    }

    public int PlayerId { get; init; }

    public int NumberOfAttackLaunched { get; set; }

    public int NumberOfAttackReceived { get; set; }
}