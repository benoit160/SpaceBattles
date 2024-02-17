using SpaceBattles.Core.Domain.Enums;
using SpaceBattles.Core.Domain.Interfaces;
using SpaceBattles.Core.Domain.Records;

namespace SpaceBattles.Core.Domain.Entities.Building;

#nullable disable annotations
public sealed class BuildingLevel : IRequirements
{
    public Building Building { get; init; }
    
    public short BuildingId { get; init; }

    public short Level { get; set; }
    
    public long TitaniumCost => (long)(Building.TitaniumCost * Math.Pow(Building.ScalingFactor, Level));
    
    public long SiliconCost  => (long)(Building.SiliconCost * Math.Pow(Building.ScalingFactor, Level));
    
    public long HeliumCost  => (long)(Building.HeliumCost * Math.Pow(Building.ScalingFactor, Level));
    
    IEnumerable<ResourceCost> IRequirements.Costs
    {
        get
        {
            yield return new ResourceCost(Resource.Titanium, TitaniumCost);
            yield return new ResourceCost(Resource.Silicon, SiliconCost);
            yield return new ResourceCost(Resource.Helium, HeliumCost);
        }
    }
}