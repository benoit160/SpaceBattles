using SpaceBattles.Core.Domain.Records;

namespace SpaceBattles.Core.Domain.Interfaces;

public interface IBuildingRequirements
{
    IEnumerable<BuildingRequirement> BuildingRequirements { get; }
}