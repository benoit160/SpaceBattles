using SpaceBattles.Core.Domain.Interfaces;

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
    
    public short OperatingLevel { get; set; }

    public int Energy => (int)(Building.BaseEnergy * Level * Math.Pow(1.1f, Level)) * (int)Building.EnergyStatus;
}