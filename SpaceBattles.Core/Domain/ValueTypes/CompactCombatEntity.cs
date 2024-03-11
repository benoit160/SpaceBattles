using SpaceBattles.Core.Domain.Entities.Battle;

namespace SpaceBattles.Core.Domain.ValueTypes;

public struct CompactCombatEntity
{
    public short _combatEntityId;

    private int _armor;
    private int _shield;
    private int _firepower;
    
    public bool IsAlive => _armor > 0;
    
    public CompactCombatEntity(CombatEntity entity)
    {
        _armor = entity.BaseArmor;
        _shield = entity.BaseShield;
        _firepower = entity.BaseWeaponPower;
        _combatEntityId = entity.Id;
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