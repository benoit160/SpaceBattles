namespace SpaceBattles.Core.Domain.Technology;

public sealed class Technology
{
    public int Id { get; init; }

    public required string Name { get; init; }

    public required string Description { get; init; }

    public int TitaniumCost { get; init; }

    public int SiliconCost { get; init; }

    public int HeliumCost { get; init; }

    /// <summary>
    /// Contains the initial technologies list.
    /// </summary>
    public static Technology[] Technologies()
    {
        return [];
    }
}