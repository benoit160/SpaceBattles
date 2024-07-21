namespace SpaceBattles.Core.Domain.Interfaces;

using SpaceBattles.Core.Domain.Records;

public interface IBuildingRequirements
{
    BuildingRequirement[] BuildingRequirements { get; }
}