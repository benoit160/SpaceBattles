using SpaceBattles.Core.Domain.Records;

namespace SpaceBattles.Core.Domain.Interfaces;

public interface IRequirements
{
    public long TitaniumCost { get; }
    
    public long SiliconCost { get; }
    
    public long HeliumCost { get; }

    public IEnumerable<ResourceCost> Costs { get; }

    public TimeSpan Duration { get; }
}