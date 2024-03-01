using System.Text.Json.Serialization;
using SpaceBattles.Core.Domain.Interfaces;

namespace SpaceBattles.Core.Domain.Entities.Building;

#nullable disable annotations
public sealed class BuildingLevel : IRequirements
{
    [JsonIgnore]
    public Building Building { get; set; }
    
    public short BuildingId { get; init; }

    public short Level { get; set; }
    
    [JsonIgnore]
    public long TitaniumCost => (long)(Building.TitaniumCost * Math.Pow(Building.ScalingFactor, Level));
    
    [JsonIgnore]
    public long SiliconCost  => (long)(Building.SiliconCost * Math.Pow(Building.ScalingFactor, Level));
    
    [JsonIgnore]
    public long HeliumCost  => (long)(Building.HeliumCost * Math.Pow(Building.ScalingFactor, Level));
}