namespace SpaceBattles.Core.Domain.Entities.Universe;

using System.Text.Json.Serialization;
using SpaceBattles.Core.Domain.Entities.Battle;
using SpaceBattles.Core.Domain.Entities.Building;
using SpaceBattles.Core.Domain.Entities.Upgrade;
using SpaceBattles.Core.Domain.Enums;
using SpaceBattles.Core.Domain.Interfaces;
using SpaceBattles.Core.Domain.Records;

public sealed class Planet : IPosition, IBattleUnitProvider
{
    // stores fractional leftover value of resources
    private readonly double[] _decimalResourcesLeft = new double[3];

    public Planet()
    {
        Name = $"Planet-{Guid.NewGuid().ToString()[..4]}";
        ImageIndex = Convert.ToByte(Random.Shared.Next(0, 14));

        PlanetType[] values = Enum.GetValues<PlanetType>();
        PlanetType = values[Random.Shared.Next(1, values.Length - 1)];

        OrbitalPeriod = Convert.ToInt16(Random.Shared.Next(50, 2000));
        AverageSurfaceTemp = Convert.ToInt16(Random.Shared.Next(1, 50));
        Gravity = Random.Shared.NextSingle() * 5;
    }

    public event Action? OnBlackOut;

    public int Id => Slot | SolarSystem << 8 | Galaxy << 16;

    [JsonIgnore]
    public Player.Player? Owner { get; set; }

    public short? OwnerId { get; set; }

    public string Name { get; set; }

    public byte ImageIndex { get; init; }

    public string ImagePath => $"/images/planets/planet{ImageIndex}.avif";

    public byte Galaxy { get; init; }

    public byte SolarSystem { get; init; }

    public byte Slot { get; init; }

    public PlanetType PlanetType { get; init; }

    public short OrbitalPeriod { get; init; }

    public short AverageSurfaceTemp { get; init; }

    public float Gravity { get; init; }

    public long Titanium { get; set; }

    public long Silicon { get; set; }

    public long Helium { get; set; }

    public BuildingLevel[] Buildings { get; set; }
        = Array.Empty<BuildingLevel>();

    public CombatEntityInventory[] BattleUnits { get; set; }
        = Array.Empty<CombatEntityInventory>();

    [JsonIgnore]
    public ReadOnlyMemory<CombatEntityInventory> Defenses { get; set; }

    [JsonIgnore]
    public ReadOnlyMemory<CombatEntityInventory> Spaceships { get; set; }

    public BuildingUpgrade? BuildingUpgrade { get; set; }

    public ShipyardConstruction? ShipyardConstruction { get; set; }

    public DateTime LastUpdated { get; set; }

    public long this[Resource resource]
    {
        get
        {
            return resource switch
            {
                Resource.Titanium => Titanium,
                Resource.Silicon => Silicon,
                Resource.Helium => Helium,
                _ => throw new ArgumentOutOfRangeException(nameof(resource), resource, null),
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

    public void DefineOwner(Player.Player player)
    {
        Owner = player;
        OwnerId = player.Id;
    }

    public void Init()
    {
        LastUpdated = DateTime.Now;

        Titanium = 150;
        Silicon = 75;

        Buildings = Building.Buildings()
            .Select(building => new BuildingLevel
            {
                BuildingId = building.Id,
                Building = building,
                OperatingLevel = 100,
            }).ToArray();

        BattleUnits = Defense.Defenses()
            .Concat<CombatEntity>(Spaceship.Spaceships())
            .Select(entity => new CombatEntityInventory
            {
                CombatEntity = entity,
                CombatEntityId = entity.Id,
            }).ToArray();

        Spaceships = BattleUnits.AsMemory(8, 10);
        Defenses = BattleUnits.AsMemory(0, 8);
    }

    /// <summary>
    /// Gets the energy production and usage of the planet.
    /// </summary>
    public (int Produced, int Usage) GetEnergyStatus()
    {
        int produced = Buildings
            .Where(b => b.Building.EnergyStatus == ElectricalEntityStatus.Producer)
            .Sum(b => b.Energy);

        int consumed = Buildings
            .Where(b => b.Building.EnergyStatus == ElectricalEntityStatus.Consummer)
            .Sum(b => b.Energy);

        return (produced, consumed);
    }

    /// <summary>
    /// Sets the building operating level, in the 0-100% range.
    /// </summary>
    public bool SetOperatingLevel(short buildingId, short level)
    {
        BuildingLevel? building = Array.Find(Buildings, b => b.BuildingId == buildingId);

        if (building is null) return false;

        building.OperatingLevel = level;

        CheckBlackOut();

        return true;
    }

    /// <summary>
    /// Adds resource to the planet inventory depending on production levels and storage capacity.
    /// </summary>
    public void ResourcesUpdate(DateTime now, Span<long> totalResources)
    {
        TimeSpan elapsedTime = now - LastUpdated;
        int elapsedSeconds = (int)elapsedTime.TotalSeconds;

        const double secondsToMinuteFraction = 1d / 60d;

        Span<Resource> resources = Enum.GetValues<Resource>();
        for (int i = 0; i < resources.Length; i++)
        {
            Resource loopResource = resources[i];
            double resourceProduced = ResourceProduction(loopResource) * secondsToMinuteFraction * elapsedSeconds;
            int resourceProducedRounded = (int)Math.Floor(resourceProduced);
            double resourceLeftover = resourceProduced - resourceProducedRounded;

            this[loopResource] += resourceProducedRounded;
            totalResources[i] += resourceProducedRounded;

            _decimalResourcesLeft[i] += resourceLeftover;

            if (_decimalResourcesLeft[i] >= 1)
            {
                this[loopResource] += 1;
                totalResources[i] += 1;
                _decimalResourcesLeft[i] -= 1;
            }
        }

        LastUpdated += TimeSpan.FromSeconds(elapsedSeconds);
    }

    /// <summary>
    /// Gets the storage capacity of the resource on this current planet.
    /// </summary>
    public long ResourceCapacity(Resource resource) => resource switch
    {
        Resource.Titanium => 5_000 * Convert.ToInt64(Math.Floor(2.5d * Math.Pow(Math.E, 20d / 33d * Buildings.First(x => x.BuildingId == 2).Level))),
        Resource.Silicon => 5_000 * Convert.ToInt64(Math.Floor(2.5d * Math.Pow(Math.E, 20d / 33d * Buildings.First(x => x.BuildingId == 4).Level))),
        Resource.Helium => 5_000 * Convert.ToInt64(Math.Floor(2.5d * Math.Pow(Math.E, 20d / 33d * Buildings.First(x => x.BuildingId == 6).Level))),
        _ => default,
    };

    /// <summary>
    /// Gets the resource production per minute of the resource on this current planet.
    /// </summary>
    public int ResourceProduction(Resource resource) => resource switch
    {
        Resource.Titanium => 30 + Convert.ToInt32(30 * Buildings.First(x => x.BuildingId == 1).Level * Math.Pow(1.1, Buildings.First(x => x.BuildingId == 1).Level)),
        Resource.Silicon => 15 + Convert.ToInt32(20 * Buildings.First(x => x.BuildingId == 3).Level * Math.Pow(1.1, Buildings.First(x => x.BuildingId == 3).Level)),
        Resource.Helium => Convert.ToInt32(11 * Buildings.First(x => x.BuildingId == 5).Level * Math.Pow(1.1, Buildings.First(x => x.BuildingId == 5).Level)),
        _ => default,
    };

    public bool MeetsBuildingRequirements(IBuildingRequirements requirements)
        => requirements.BuildingRequirements.All(x => Buildings.Single(b => b.BuildingId == x.BuildingId).Level >= x.RequiredLevel);

    public bool HasEnoughResource(IRequirements requirements, int quantity = 1)
        => requirements.Costs.All(cost => this[cost.Resource] >= cost.RequiredQuantity * quantity);

    public void ConsumeResources(IRequirements requirements, int quantity = 1)
    {
        foreach (ResourceCost cost in requirements.Costs)
        {
            this[cost.Resource] -= cost.RequiredQuantity * quantity;
        }
    }

    public BuildingLevel? ProcessUpgrades(DateTime now)
    {
        if (BuildingUpgrade is not null && BuildingUpgrade.End <= now)
        {
            BuildingLevel upgradedBuilding = Buildings.Single(x => x.BuildingId == BuildingUpgrade.BuildingId);
            upgradedBuilding.Level++;
            BuildingUpgrade = null;
            return upgradedBuilding;
        }

        return null;
    }

    public bool ProcessShipyard(DateTime now)
    {
        if (ShipyardConstruction is not null && ShipyardConstruction.End <= now)
        {
            CombatEntityInventory construction = BattleUnits.Single(x => x.CombatEntityId == ShipyardConstruction.CombatEntityId);
            construction.Quantity += ShipyardConstruction.Quantity;
            ShipyardConstruction = null;
            return true;
        }

        return false;
    }

    public bool CanUpgradeBuilding(short buildingId)
    {
        if (BuildingUpgrade is not null) return false;

        BuildingLevel? level = Array.Find(Buildings, buildingLevel => buildingLevel.BuildingId == buildingId);

        if (level is null) return false;

        return HasEnoughResource(level);
    }

    public bool TryUpgradeBuilding(short buildingId)
    {
        IRequirements? level = Array.Find(Buildings, buildingLevel => buildingLevel.BuildingId == buildingId);

        if (level is null) return false;

        if (!HasEnoughResource(level)) return false;

        if (BuildingUpgrade is not null) return false;

        ConsumeResources(level);

        BuildingUpgrade upgrade = new BuildingUpgrade()
        {
            BuildingId = buildingId,
            Start = DateTime.Now,
            Duration = level.Duration(Buildings.First(b => b.BuildingId == 8).Level),
        };

        BuildingUpgrade = upgrade;

        return true;
    }

    public bool TryConstructShipyard(short combatEntityId, short quantity)
    {
        CombatEntity? entity = Array.Find(BattleUnits, unit => unit.CombatEntityId == combatEntityId)?.CombatEntity;

        if (entity is null) return false;

        if (!MeetsBuildingRequirements(entity)) return false;

        if (!HasEnoughResource(entity, quantity)) return false;

        if (ShipyardConstruction is not null) return false;

        ConsumeResources(entity, quantity);

        ShipyardConstruction construction = new ShipyardConstruction()
        {
            Start = DateTime.Now,
            CombatEntityId = combatEntityId,
            Quantity = quantity,
            Duration = (entity as IRequirements).Duration(Buildings.First(b => b.BuildingId == 9).Level) * quantity,
        };

        ShipyardConstruction = construction;

        return true;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(ImageIndex, Galaxy, SolarSystem, Slot, PlanetType, OrbitalPeriod, AverageSurfaceTemp, Gravity);
    }

    /// <summary>
    /// Disables all buildings when the energy usage  is higher than the planet production.
    /// </summary>
    private void CheckBlackOut()
    {
        (int Produced, int Usage) energy = GetEnergyStatus();

        if (Math.Abs(energy.Usage) > energy.Produced)
        {
            OnBlackOut?.Invoke();
            Array.ForEach(Buildings, b => b.OperatingLevel = 0);
        }
    }
}