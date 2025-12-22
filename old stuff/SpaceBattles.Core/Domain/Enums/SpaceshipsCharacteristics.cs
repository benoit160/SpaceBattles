namespace SpaceBattles.Core.Domain.Enums;

[Flags]
public enum SpaceshipsCharacteristics : byte
{
    Combustion = 1,
    Impulse = 2,
    Hyperspace = 4,

    PropulsionType = Combustion | Impulse | Hyperspace,

    Fighters = 8,
    Warships = 16,
    Utility = 32,

    Category = Fighters | Warships | Utility,
}