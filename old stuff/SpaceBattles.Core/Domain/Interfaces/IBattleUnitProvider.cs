namespace SpaceBattles.Core.Domain.Interfaces;

using SpaceBattles.Core.Domain.Entities.Battle;

public interface IBattleUnitProvider
{
    public CombatEntityInventory[] BattleUnits { get; }
}