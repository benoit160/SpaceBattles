using SpaceBattles.Core.Domain.Enums;

namespace SpaceBattles.Core.Domain.Entities.Battle;

public sealed class Spaceship : CombatEntity
{
    public SpaceshipsCharacteristics Characteristics { get; init; }

    public int BaseSpeed { get; init; }

    public int CargoCapacity { get; init; }

    public int FuelUsage { get; init; }
}