﻿using SpaceBattles.Core.Domain.Entities.Building;
using SpaceBattles.Core.Domain.Enums;

namespace SpaceBattles.Core.Domain.Entities.Universe;

public sealed class Planet
{
    public Planet()
    {
        Name = "Earth";
        ImageIndex = Convert.ToByte(Random.Shared.Next(0, 10));
        
        PlanetType[] values = Enum.GetValues<PlanetType>();
        PlanetType = values[Random.Shared.Next(values.Length)];
        
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
    
    // stores fractional leftover value of resources
    private readonly double[] _decimalResourcesLeft = new double[3]; 
}