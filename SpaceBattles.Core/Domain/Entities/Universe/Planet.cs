using SpaceBattles.Core.Domain.Entities.Building;
using SpaceBattles.Core.Domain.Entities.Upgrade;
using SpaceBattles.Core.Domain.Enums;
using SpaceBattles.Core.Domain.Interfaces;
using SpaceBattles.Core.Domain.Records;

namespace SpaceBattles.Core.Domain.Entities.Universe;

public sealed class Planet
{
    public Planet()
    {
        Name = "Earth";
        ImageIndex = Convert.ToByte(Random.Shared.Next(0, 10));
        
        PlanetType[] values = Enum.GetValues<PlanetType>();
        PlanetType = values[Random.Shared.Next(1, values.Length - 1)];
        
        OrbitalPeriod = Convert.ToInt16(Random.Shared.Next(50, 2000));
        AverageSurfaceTemp = Convert.ToInt16(Random.Shared.Next(1, 50));
        Gravity = Random.Shared.NextSingle() * 5;
        
        LastUpdated = DateTime.Now;
        
        Titanium = 150;
        Silicon = 75;
        
        Buildings = Building.Building.Buildings()
            .Select(building => new BuildingLevel
            {
                BuildingId = building.Id,
                Building = building,
            }).ToList();
    }
    
    public string Name { get; set; }

    public byte ImageIndex { get; init; }

    public string ImagePath => $"/images/planets/planet{ImageIndex}.avif";

    public PlanetType PlanetType { get; init; }
    public short OrbitalPeriod { get; init; }
    public short AverageSurfaceTemp { get; init; }
    public float Gravity { get; init; }

    public long this[Resource resource]
    {
        get
        {
            return resource switch
            {
                Resource.Titanium => Titanium,
                Resource.Silicon => Silicon,
                Resource.Helium => Helium,
                _ => throw new ArgumentOutOfRangeException(nameof(resource), resource, null)
            };
        }
        set
        {
            long max = Math.Min(value, ResourceCapacity(resource));

            switch (resource)
            {
                case Resource.Titanium:
                    Titanium = max;
                    break;
                case Resource.Silicon:
                    Silicon = max;
                    break;
                case Resource.Helium:
                    Helium = max;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(resource), resource, null);
            }
        }
    }

    public long Titanium { get; private set; }
    
    public long Silicon { get; private set; }
    
    public long Helium { get; private set; }
    
    public List<BuildingLevel> Buildings { get; }

    public BuildingUpgrade? BuildingUpgrade { get; private set; }

    public DateTime LastUpdated { get; private set; }

    /// <summary>
    /// Adds resource to the planet inventory depending on production levels and storage capacity 
    /// </summary>
    public void ResourcesUpdate(DateTime now)
    {
        TimeSpan elapsedTime = now - LastUpdated;
        int elapsedSeconds = (int)elapsedTime.TotalSeconds;

        const double secondsToMinuteFraction = 1d / 60d;

        var resources = Enum.GetValues<Resource>();
        for (int i = 0; i < resources.Length; i++)
        {
            Resource loopResource = resources[i];
            double resourceProduced = ResourceProduction(loopResource) * secondsToMinuteFraction * elapsedSeconds;
            int resourceProducedRounded = (int)Math.Floor(resourceProduced);
            double resourceLeftover = resourceProduced - resourceProducedRounded;

            this[loopResource] += resourceProducedRounded;

            _decimalResourcesLeft[i] += resourceLeftover;

            if (_decimalResourcesLeft[i] >= 1)
            {
                this[loopResource] += 1;
                _decimalResourcesLeft[i] -= 1;
            }
        }

        LastUpdated += TimeSpan.FromSeconds(elapsedSeconds);
    }
    
    /// <summary>
    /// Gets the storage capacity of the resource on this current planet
    /// </summary>
    public long ResourceCapacity(Resource resource) => resource switch
    {
        Resource.Titanium => 5_000 * Convert.ToInt64(Math.Floor(2.5d * Math.Pow(Math.E, 20d / 33d * Buildings.First(x => x.BuildingId == 2).Level))),
        Resource.Silicon => 5_000 * Convert.ToInt64(Math.Floor(2.5d * Math.Pow(Math.E, 20d / 33d * Buildings.First(x => x.BuildingId == 4).Level))),
        Resource.Helium => 5_000 * Convert.ToInt64(Math.Floor(2.5d * Math.Pow(Math.E, 20d / 33d * Buildings.First(x => x.BuildingId == 6).Level))),
        _ => default
    };

    /// <summary>
    /// Gets the resource production per minute of the resource on this current planet
    /// </summary>
    public int ResourceProduction(Resource resource) => resource switch
    {
        Resource.Titanium => 30 + Convert.ToInt32(30 * Buildings.First(x => x.BuildingId == 1).Level * Math.Pow(1.1, Buildings.First(x => x.BuildingId == 1).Level)),
        Resource.Silicon => 15 + Convert.ToInt32(20 * Buildings.First(x => x.BuildingId == 3).Level * Math.Pow(1.1, Buildings.First(x => x.BuildingId == 3).Level)),
        Resource.Helium => Convert.ToInt32(11 * Buildings.First(x => x.BuildingId == 5).Level * Math.Pow(1.1, Buildings.First(x => x.BuildingId == 5).Level)),
        _ => default
    };
    
    public bool HasEnoughResource(IRequirements requirements)
        => requirements.Costs.All(cost => this[cost.Resource] >= cost.RequiredQuantity);
    
    public void ConsumeResources(IRequirements requirements)
    {
        foreach (ResourceCost cost in requirements.Costs)
        {
            this[cost.Resource] -= cost.RequiredQuantity;
        }
    }
    
    public void ProcessUpgrades(DateTime now)
    {
        if (BuildingUpgrade is not null && BuildingUpgrade.End <= now)
        {
            Buildings.Single(x => x.BuildingId == BuildingUpgrade.BuildingId).Level++;
            BuildingUpgrade = null;
        }
    }
    
    public bool CanUpgradeBuilding(short buildingId)
    {
        if (BuildingUpgrade is not null) return false;

        BuildingLevel? level = Buildings.Find(bl => bl.BuildingId == buildingId);

        if (level is null) return false;

        return HasEnoughResource(level);
    }
    
    public bool TryUpgradeBuilding(short buildingId)
    {
        IRequirements? level = Buildings.Find(bl => bl.BuildingId == buildingId);

        if (level is null) return false;

        if (!HasEnoughResource(level)) return false;

        if (BuildingUpgrade is not null) return false;

        ConsumeResources(level);

        BuildingUpgrade upgrade = new BuildingUpgrade()
        {
            BuildingId = buildingId,
            Start = DateTime.Now,
            Duration = level.Duration,
        };

        BuildingUpgrade = upgrade;
        
        return true;
    }
    
    // stores fractional leftover value of resources
    private readonly double[] _decimalResourcesLeft = new double[3]; 
}