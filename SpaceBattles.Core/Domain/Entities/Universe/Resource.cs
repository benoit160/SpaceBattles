using SpaceBattles.Core.Domain.Enums;
using SpaceBattles.Core.Domain.Records;

namespace SpaceBattles.Core.Domain.Entities.Universe;

public static class Resources
{
    public static IEnumerable<ResourceInfo> ResourcesDisplay()
    {
        yield return new ResourceInfo(Resource.Titanium, "Titanium", "/images/items/titanium.avif");
        yield return new ResourceInfo(Resource.Silicon, "Silicon", "/images/items/silicon.avif");
        yield return new ResourceInfo(Resource.Helium, "Helium", "/images/items/helium.avif");
    }
}