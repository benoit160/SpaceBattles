namespace SpaceBattles.Core.Domain.Interfaces;

using SpaceBattles.Core.Domain.Records;

public interface IBuildingRequirements
{
    IEnumerable<BuildingRequirement> BuildingRequirements { get; }
}