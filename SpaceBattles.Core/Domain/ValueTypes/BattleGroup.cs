using SpaceBattles.Core.Domain.Entities.Battle;
using SpaceBattles.Core.Domain.Interfaces;

namespace SpaceBattles.Core.Domain.ValueTypes;

public struct BattleGroup
{
    private readonly CompactCombatEntity[] _units;
    
    public int Length => _units.Length;

    public CompactCombatEntity this[int i] => _units[i];
    
    public bool IsAlive => _units.Any(x => x.IsAlive);

    public ref CompactCombatEntity RandomUnit => ref _units[Random.Shared.Next(Length)];

    public BattleGroup(ICombatEntityInventoryProvider inventoryProvider)
    {
        int totalUnitCount = inventoryProvider.Inventory.Sum(inv => inv.Quantity);
        _units = new CompactCombatEntity[totalUnitCount];

        int currentIndex = 0;

        foreach (CombatEntityInventory combatEntityInventory in inventoryProvider.Inventory)
        {
            Memory<CompactCombatEntity> slice = _units.AsMemory().Slice(currentIndex, combatEntityInventory.Quantity);
            
            for (int i = 0; i < slice.Length; i++)
            {
                slice.Span[i] = new CompactCombatEntity(combatEntityInventory.CombatEntity);
            }

            currentIndex += combatEntityInventory.Quantity;
        }
    }
}