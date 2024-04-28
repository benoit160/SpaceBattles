namespace SpaceBattles.Core.Application.Services;

public sealed class PlanetStatistics
{
    public PlanetStatistics(int planetId)
    {
        PlanetId = planetId;
    }

    public int PlanetId { get; init; }

    public long TotalTitaniumProduced { get; set; }

    public long TotalSiliconProduced { get; set; }

    public long TotalHeliumProduced { get; set; }
}