namespace SpaceBattles.Core.Domain.GameSystems;

using SpaceBattles.Core.Domain.Interfaces;
using SpaceBattles.Core.Domain.ValueTypes;

public sealed class Battle
{
    private BattleGroup _attacker;
    private BattleGroup _defender;

    private int _currentTurn;

    public Battle(IBattleUnitProvider attacker, IBattleUnitProvider defender)
    {
        _attacker = new BattleGroup(attacker);
        _defender = new BattleGroup(defender);
    }

    public bool IsFinished()
    {
        return !_attacker.IsAlive || !_defender.IsAlive;
    }

    public void NextTurn()
    {
        _currentTurn++;

        for (int i = 0; i < _attacker.Length; i++)
        {
            if (!_attacker[i].IsAlive) continue;
            _attacker[i].FireAt(ref _defender.RandomUnit);
        }

        for (int i = 0; i < _defender.Length; i++)
        {
            if (!_defender[i].IsAlive) continue;
            _defender[i].FireAt(ref _attacker.RandomUnit);
        }
    }
}