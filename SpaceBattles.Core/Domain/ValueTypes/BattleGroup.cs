using SpaceBattles.Core.Domain.Interfaces;

namespace SpaceBattles.Core.Domain.ValueTypes;

public struct BattleGroup
{
    private readonly CompactCombatEntity[] _units;
    
    public int Length => _units.Length;
    
    public bool IsAlive => _units.Any(x => x.IsAlive);

    public ref CompactCombatEntity RandomUnit => ref _units[Random.Shared.Next(Length)];

    public BattleGroup(ICombatEntityInventoryProvider inventoryProvider)
    {
        int totalUnitCount = inventoryProvider.Inventory.Sum(inv => inv.Quantity);
        _units = new CompactCombatEntity[totalUnitCount];
    }
}