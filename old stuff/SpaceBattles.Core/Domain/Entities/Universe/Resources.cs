namespace SpaceBattles.Core.Domain.Entities.Universe;

using SpaceBattles.Core.Domain.Enums;
using SpaceBattles.Core.Domain.Records;

public static class Resources
{
    public static ResourceInfo[] ResourceInfos { get; } =
    [
        new ResourceInfo(Resource.Titanium, "Titanium", "/images/items/titanium.avif"),
        new ResourceInfo(Resource.Silicon, "Silicon", "/images/items/silicon.avif"),
        new ResourceInfo(Resource.Helium, "Helium", "/images/items/helium.avif"),
    ];
}