using SpaceBattles.Core.Domain.Entities.Battle;

namespace SpaceBattles.Core.Domain.Interfaces;

public interface ICombatEntityInventoryProvider
{
    public IEnumerable<CombatEntityInventory> Inventory { get; }
}