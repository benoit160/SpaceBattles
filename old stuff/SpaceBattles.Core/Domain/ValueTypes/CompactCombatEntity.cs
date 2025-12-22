namespace SpaceBattles.Core.Domain.ValueTypes;

using SpaceBattles.Core.Domain.Entities.Battle;

public struct CompactCombatEntity
{
    private int _armor;
    private int _shield;
    private int _firepower;

    public CompactCombatEntity(CombatEntity entity)
    {
        _armor = entity.BaseArmor;
        _shield = entity.BaseShield;
        _firepower = entity.BaseWeaponPower;
        CombatEntityId = entity.Id;
    }

    public bool IsAlive => _armor > 0;

    public short CombatEntityId { get; }

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