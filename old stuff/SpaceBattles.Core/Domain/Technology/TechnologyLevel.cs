namespace SpaceBattles.Core.Domain.Technology;

using System.Text.Json.Serialization;
using SpaceBattles.Core.Domain.Interfaces;

public sealed class TechnologyLevel : IRequirements
{
    public int TechnologyId { get; init; }

    [JsonIgnore]
    public required Technology Technology { get; set; }

    public short Level { get; set; }

    public long TitaniumCost => (long)(Technology.TitaniumCost * Math.Pow(2, Level));

    public long SiliconCost => (long)(Technology.SiliconCost * Math.Pow(2, Level));

    public long HeliumCost => (long)(Technology.HeliumCost * Math.Pow(2, Level));
}