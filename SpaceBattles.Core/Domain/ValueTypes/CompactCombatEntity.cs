namespace SpaceBattles.Core.Domain.ValueTypes;

public struct CompactCombatEntity
{
    public short _combatEntityId;

    private int _armor;
    private int _shield;
    private int _firepower;
    
    public bool IsAlive => _armor > 0;

    public CompactCombatEntity(int armor, int shield, int weapon, short id)
    {
        _armor = armor;
        _shield = shield;
        _firepower = weapon;
        _combatEntityId = id;
    }
    
    public void FireAt(ref CompactCombatEntity target)
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