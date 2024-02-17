namespace SpaceBattles.Core.Domain.Entities.Building;

#nullable disable annotations
public sealed class BuildingLevel
{
    public Building Building { get; init; }
    
    public short BuildingId { get; init; }

    public short Level { get; set; }
}