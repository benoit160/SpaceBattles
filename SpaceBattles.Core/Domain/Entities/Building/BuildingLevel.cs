namespace SpaceBattles.Core.Domain.Entities.Building;

using System.Text.Json.Serialization;
using SpaceBattles.Core.Domain.Interfaces;

#nullable disable annotations
public sealed class BuildingLevel : IRequirements
{
    [JsonIgnore]
    public Building Building { get; set; }

    public short BuildingId { get; init; }

    public short Level { get; set; }

    public long TitaniumCost => (long)(Building.TitaniumCost * Math.Pow(Building.ScalingFactor, Level));

    public long SiliconCost => (long)(Building.SiliconCost * Math.Pow(Building.ScalingFactor, Level));

    public long HeliumCost => (long)(Building.HeliumCost * Math.Pow(Building.ScalingFactor, Level));

    public short OperatingLevel { get; set; }

    public int Energy => Convert.ToInt32(Building.BaseEnergy * Level * Math.Pow(1.1f, Level)) * (int)Building.EnergyStatus;
}