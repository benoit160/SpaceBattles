namespace SpaceBattles.Core.Domain.Entities.Battle;

public class CombatEntityInventory
{
    public required CombatEntity CombatEntity { get; init; }
    
    public short CombatEntityId { get; init; }

    public int Quantity { get; set; }
}