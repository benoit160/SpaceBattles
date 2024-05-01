namespace SpaceBattles.Core.Domain.Interfaces;

using SpaceBattles.Core.Domain.Enums;
using SpaceBattles.Core.Domain.Records;

public interface IRequirements
{
    public long TitaniumCost { get; }

    public long SiliconCost { get; }

    public long HeliumCost { get; }

    IEnumerable<ResourceCost> Costs
    {
        get
        {
            yield return new ResourceCost(Resource.Titanium, TitaniumCost);
            yield return new ResourceCost(Resource.Silicon, SiliconCost);
            yield return new ResourceCost(Resource.Helium, HeliumCost);
        }
    }

    public TimeSpan Duration(int buildingLevel)
    {
        double reductionFactor = 2500d * (1 + buildingLevel);
        double durationHours = (TitaniumCost + SiliconCost) / reductionFactor;
        return TimeSpan.FromHours(durationHours);
    }
}