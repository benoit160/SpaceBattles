namespace SpaceBattles.Core.Domain.ValueTypes;

using SpaceBattles.Core.Domain.Entities.Battle;
using SpaceBattles.Core.Domain.Interfaces;

public struct BattleGroup
{
    private readonly CompactCombatEntity[] _units;

    public BattleGroup(IBattleUnitProvider inventoryProvider)
    {
        int totalUnitCount = inventoryProvider.BattleUnits.Sum(inv => inv.Quantity);
        _units = new CompactCombatEntity[totalUnitCount];

        int currentIndex = 0;

        foreach (CombatEntityInventory combatEntityInventory in inventoryProvider.BattleUnits)
        {
            Memory<CompactCombatEntity> slice = _units.AsMemory().Slice(currentIndex, combatEntityInventory.Quantity);

            for (int i = 0; i < slice.Length; i++)
            {
                slice.Span[i] = new CompactCombatEntity(combatEntityInventory.CombatEntity);
            }

            currentIndex += combatEntityInventory.Quantity;
        }
    }

    public int Length => _units.Length;

    public bool IsAlive => _units.Any(x => x.IsAlive);

    public ref CompactCombatEntity RandomUnit => ref _units[Random.Shared.Next(Length)];

    public CompactCombatEntity this[int i] => _units[i];
}