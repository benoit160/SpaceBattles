using SpaceBattles.Core.Domain.ValueTypes;

namespace SpaceBattles.Core.Domain.Interfaces;

public interface IBuildingRequirements
{
    IEnumerable<BuildingRequirement> BuildingRequirements { get; }
}