namespace SpaceBattles.Core.Domain.Interfaces;

public interface IPosition
{
    public byte Galaxy { get; init; }

    public byte SolarSystem { get; init; }

    public byte Slot { get; init; }

    /// <summary>
    /// Computes the distance to the other object, to determine duration of spaceflight between.
    /// </summary>
    public int DistanceTo(IPosition other)
    {
        // Distance between a planet and it's moon for exemple
        if (Equals(other))
            return 5;

        int distanceBetweenGalaxies = Galaxy == other.Galaxy ?
            0 : Math.Abs(Galaxy - other.Galaxy) * 20_000;

        int distanceBetweenSolarSystems = SolarSystem == other.SolarSystem ?
            0 : 2_700 + (Math.Abs(SolarSystem - other.SolarSystem) * 95);

        int distanceBetweenSlots = Slot == other.Slot ?
            0 : 1_000 + (Math.Abs(Slot - other.Slot) * 5);

        return distanceBetweenGalaxies + distanceBetweenSolarSystems + distanceBetweenSlots;
    }

    /// <summary>
    /// Computes the distance between the 2 objects, to determine duration of spaceflight between.
    /// </summary>
    public static int DistanceBetween(IPosition first, IPosition second) => first.DistanceTo(second);

    /// <summary>
    /// Indicates whether the coordinates points to the same position.
    /// </summary>
    public bool Equals(IPosition? other)
    {
        return other is not null && (Slot == other.Slot && Galaxy == other.Galaxy && SolarSystem == other.SolarSystem);
    }
}