namespace SpaceBattles.Core.Application.Services;

public sealed class PlanetStatistics
{
    public long TotalTitaniumProduced { get; set; }

    public long TotalSiliconProduced { get; set; }

    public long TotalHeliumProduced { get; set; }

    public int NumberOfAttackLaunched { get; set; }

    public int NumberOfAttackReceived { get; set; }
}