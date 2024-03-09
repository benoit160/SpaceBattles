using SpaceBattles.Core.Domain.Interfaces;
using SpaceBattles.Core.Domain.ValueTypes;

namespace SpaceBattles.Core.Domain.Entities.Battle;

public abstract class CombatEntity : IBuildingRequirements, IRequirements
{
    public short Id { get; init; }

    public required string ImageName { get; init; }

    public string ImagePath => $"/images/combat/{ImageName}";
    
    public required string Name { get; init; }

    public required string Description { get; init; }
    
    public int BaseArmor { get; init; }
    public int BaseShield { get; init; }
    public int BaseWeaponPower { get; init; }

    public RapidFireAgainst[] RapidFires { get; init; }
        = Array.Empty<RapidFireAgainst>();
    
    public IEnumerable<BuildingRequirement> BuildingRequirements { get; init; }
        = Enumerable.Empty<BuildingRequirement>();

    public long TitaniumCost { get; init; }
    public long SiliconCost { get; init; }
    public long HeliumCost { get; init; }
}