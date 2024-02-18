using SpaceBattles.Core.Domain.Enums;
using SpaceBattles.Core.Domain.Records;

namespace SpaceBattles.Core.Domain.Interfaces;

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
    
    public TimeSpan Duration
    {
        get
        {
            double durationHours = (TitaniumCost + SiliconCost) / 2500d;
            return TimeSpan.FromHours(durationHours);
        }
    }
}