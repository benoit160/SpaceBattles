namespace SpaceBattles.Core.Domain.Entities.Battle;

public struct s_CombatEntity
{
    public short CombatEntityId;

    private int _armor;
    private int _shield;
    private int _firepower;

    public bool IsAlive => _armor > 0;

    public s_CombatEntity(int armor, int shield, int weapon, short id)
    {
        _armor = armor;
        _shield = shield;
        _firepower = weapon;
        CombatEntityId = id;
    }

    public void FireAt(ref s_CombatEntity target)
    {
        target.TakeDamage(_firepower);
    }

    public void TakeDamage(int damage)
    {
        if (damage > _shield)
        {
            int remainder = damage - _shield;
            _shield = 0;
            _armor -= remainder;
        }
        else
        {
            _shield -= damage;
        }
    }
}