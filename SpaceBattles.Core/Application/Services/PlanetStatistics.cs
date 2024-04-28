namespace SpaceBattles.Core.Application.Services;

public sealed class PlanetStatistics
{
    public int PlanetId { get; init; }

    public long TotalTitaniumProduced { get; set; }

    public long TotalSiliconProduced { get; set; }

    public long TotalHeliumProduced { get; set; }
}


public sealed class PlayerStatistics
{
    public int PlayerId { get; init; }

    public int NumberOfAttackLaunched { get; set; }

    public int NumberOfAttackReceived { get; set; }
}