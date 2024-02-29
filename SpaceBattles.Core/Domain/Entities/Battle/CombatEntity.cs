namespace SpaceBattles.Core.Domain.Entities.Battle;

public abstract class CombatEntity
{
    public short Id { get; init; }

    public required string ImageName { get; init; }

    public string ImagePath => $"/images/combat/{ImageName}";
    
    public required string Name { get; set; }

    public required string Description { get; set; }

    public short RequiredShipyardLevel { get; set; }

    public int BaseArmor { get; set; }
    public int BaseShield { get; set; }
    public int BaseWeaponPower { get; set; }

    public RapidFireAgainst[] RapidFires { get; init; }
}

public record struct RapidFireAgainst(short CombatEntityId, short RapidFireValue);

public sealed class Spaceship : CombatEntity
{
    public int BaseSpeed { get; set; }

    public int CargoCapacity { get; set; }

    public int FuelUsage { get; set; }
}

public sealed class Defense : CombatEntity;

public class CombatEntityInventory
{
    public required CombatEntity CombatEntity { get; init; }
    
    public short CombatEntityId { get; init; }

    public int Quantity { get; set; }
}