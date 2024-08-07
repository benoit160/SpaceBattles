﻿using SpaceBattles.Core.Application.Extensions;
using SpaceBattles.Core.Domain.Entities.Battle;
using SpaceBattles.Core.Domain.Entities.Player;
using SpaceBattles.Core.Domain.Entities.Universe;
using SpaceBattles.Core.Domain.Enums;

namespace SpaceBattles.Tests.Domain.Universe;

public class PlanetTests
{
    const int OutOfRangeIndex = 27;
    
    [Theory]
    [InlineData(1,1,1,0x00010101)]
    [InlineData(1,2,3,0x00030201)]
    public void Id(byte slot, byte solar, byte galaxy, int expected)
    {
        // Arrange
        Planet planet = new Planet()
        {
            Slot = slot,
            SolarSystem = solar,
            Galaxy = galaxy,
        };
        
        // Assert
        Assert.Equal(expected, planet.Id);
    }
    
    [Fact]
    public void PlanetConstructor()
    {
        // Arrange
        Planet planet = new Planet();
        
        // Assert
        Assert.False(string.IsNullOrWhiteSpace(planet.Name));
        Assert.NotEqual(default, planet.PlanetType);
        Assert.Equal(default, planet.LastUpdated);
        
        Assert.Empty(planet.Buildings);
        Assert.Empty(planet.BattleUnits);
        Assert.Equal(0, planet.Titanium);
        Assert.Equal(0, planet.Silicon);
        Assert.Equal(0, planet.Helium);
    }
    
    [Fact]
    public void Planet_Init()
    {
        // Arrange
        Planet planet = new Planet();
        planet.Init();
        
        // Assert
        Assert.False(string.IsNullOrWhiteSpace(planet.Name));
        Assert.NotEqual(default, planet.PlanetType);
        Assert.NotEqual(default, planet.LastUpdated);
        
        Assert.Equal(12, planet.Buildings.Length);
        Assert.Equal(150, planet.Titanium);
        Assert.Equal(75, planet.Silicon);
        Assert.Equal(0, planet.Helium);
    }
    
    [Fact]
    public void BaseResourceProduction()
    {
        // Arrange
        Planet planet = new();
        planet.Init();
        
        // Assert
        Assert.Equal(30, planet.ResourceProduction(Resource.Titanium));
        Assert.Equal(15, planet.ResourceProduction(Resource.Silicon));
        Assert.Equal(0, planet.ResourceProduction(Resource.Helium));
    }
    
    [Fact]
    public void BaseResourceCapacity()
    {
        // Arrange
        Planet planet = new();
        planet.Init();
        
        // Assert
        Assert.Equal(10_000, planet.ResourceCapacity(Resource.Titanium));
        Assert.Equal(10_000, planet.ResourceCapacity(Resource.Silicon));
        Assert.Equal(10_000, planet.ResourceCapacity(Resource.Helium));
    }
    
    [Fact]
    public void MaxStorageNeverExceeded()
    {
        // Arrange
        Planet planet = new Planet();
        planet.Init();
        Array.ForEach(planet.Buildings, b => b.Level++);
        Span<long> totals = [0, 0, 0];
        
        // Act
        planet.ResourcesUpdate(DateTime.Now + TimeSpan.FromDays(100), totals);
        
        // Assert
        Assert.Equal(20_000, planet[Resource.Titanium]);
        Assert.Equal(20_000, planet[Resource.Silicon]);
        Assert.Equal(20_000, planet[Resource.Helium]);
    }
    
    [Fact]
    public void OutOfRangeIndexerGet()
    {
        // Arrange
        Planet planet = new Planet();
        planet.Init();

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            _ = planet[0];
        });
    }
    
    [Fact]
    public void OutOfRangeIndexerSet()
    {
        // Arrange
        Planet planet = new Planet();
        planet.Init();

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            planet[(Resource)OutOfRangeIndex] = 1;
        });
    }
    
    [Fact]
    public void OutOfRange_ResourceCapacity()
    {
        // Arrange
        Planet planet = new Planet();
        planet.Init();

        // Act
        var result = planet.ResourceCapacity((Resource)OutOfRangeIndex);
        
        // Assert
        Assert.Equal(0, result);
    }
    
    [Fact]
    public void OutOfRange_ResourceProduction()
    {
        // Arrange
        Planet planet = new Planet();
        planet.Init();

        // Act
        var result = planet.ResourceProduction((Resource)OutOfRangeIndex);
        
        // Assert
        Assert.Equal(0, result);
    }
    
    [Fact]
    public void ResourceUpdate()
    {
        // Arrange
        Planet planet = new Planet();
        planet.Init();
        DateTime advancedTime = DateTime.Now + TimeSpan.FromHours(1);
        Span<long> totals = [0, 0, 0];

        // Act
        planet.ResourcesUpdate(advancedTime, totals);
        
        // Assert
        Assert.Equal(180, planet[Resource.Titanium]);
        Assert.Equal(90, planet[Resource.Silicon]);
        Assert.Equal(0, planet[Resource.Helium]);
        
        advancedTime += TimeSpan.FromSeconds(5);
        
        planet.ResourcesUpdate(advancedTime, totals);
    }

    [Theory]
    [InlineData(0, 0, 0)]
    [InlineData(1, 22, -44)]
    public void GetEnergyStatus(byte level, int expectedProduction, int expectedUsage)
    {
        // Arrange
        Planet planet = new Planet();
        planet.Init();
        Array.ForEach(planet.Buildings, b => b.Level = level);

        // Act
        (int Produced, int Usage) energy = planet.GetEnergyStatus();

        // Assert
        Assert.Equal(expectedUsage, energy.Usage);
        Assert.Equal(expectedProduction, energy.Produced);
    }

    [Fact]
    public void SetOperatingLevel_InvalidId()
    {
        // Arrange
        Planet planet = new Planet();
        planet.Init();
        const int invalidId = 27;

        // Act
        bool result = planet.SetOperatingLevel(invalidId, 100);

        // Assert
        Assert.False(result);
    }
    
    [Fact]
    public void SetOperatingLevel()
    {
        // Arrange
        Planet planet = new Planet();
        planet.Init();

        bool eventRaised = false;

        void OnAction() => eventRaised = true;
        planet.OnBlackOut += OnAction;

        // Act
        planet.SetOperatingLevel(1, 100);

        // Assert
        Assert.False(eventRaised);
    }
    
    [Fact]
    public void SetOperatingLevel_Blackout()
    {
        // Arrange
        Planet planet = new Planet();
        planet.Init();
        Array.ForEach(planet.Buildings, b => b.Level = 5);
        Array.ForEach(planet.Buildings, b => b.OperatingLevel = 0);

        bool eventRaised = false;

        void OnAction() => eventRaised = true;
        planet.OnBlackOut += OnAction;

        // Act
        planet.SetOperatingLevel(1, 100);

        // Assert
        Assert.True(eventRaised);
    }

    [Fact]
    public void TransferAllSpaceshipToFleet()
    {
        // Arrange
        Planet planet = new Planet();
        planet.Init();
        
        Array.ForEach(planet.BattleUnits, bu => bu.Quantity = 10);

        Player player = new Player
        {
            Name = "Test",
        };

        planet.DefineOwner(player);

        Fleet fleet = new Fleet
        {
            OwnerId = player.Id,
            Position = new Position(planet.Galaxy, planet.SolarSystem, planet.Slot),
        };

        // Act
        planet.TransferAllSpaceshipToFleet(fleet);

        //Assert
        Assert.Equal(planet.Spaceships.Length, fleet.Spaceships.Count);
        Assert.True(fleet.Spaceships.All(s => s.Quantity == 10));
        
        Assert.True(planet.Spaceships.Span.All(s => s.Quantity == 0));
        Assert.False(planet.Defenses.Span.Any(s => s.Quantity == 0));
    }
    
    [Theory]
    [InlineData(666, 10)]
    [InlineData(11, 500)]
    public void TransferSpaceshipToFleet_Invalid(short id, short quantity)
    {
        // Arrange
        Planet planet = new Planet();
        planet.Init();
        
        Array.ForEach(planet.BattleUnits, bu => bu.Quantity = 10);

        Player player = new Player
        {
            Name = "Test",
        };

        planet.DefineOwner(player);

        Fleet fleet = new Fleet
        {
            OwnerId = player.Id,
            Position = new Position(planet.Galaxy, planet.SolarSystem, planet.Slot),
        };

        // Act
        bool result = planet.TransferSpaceshipToFleet(fleet, id, quantity);

        //Assert
        Assert.False(result);
        Assert.Empty(fleet.Spaceships);
        
        Assert.True(planet.BattleUnits.All(s => s.Quantity == 10));
    }
    
    [Fact]
    public void TransferSpaceshipToFleet()
    {
        // Arrange
        Planet planet = new Planet();
        planet.Init();
        
        Array.ForEach(planet.BattleUnits, bu => bu.Quantity = 10);

        Player player = new Player
        {
            Name = "Test",
        };

        planet.DefineOwner(player);

        Fleet fleet = new Fleet
        {
            OwnerId = player.Id,
            Position = new Position(planet.Galaxy, planet.SolarSystem, planet.Slot),
        };

        // Act
        bool result = planet.TransferSpaceshipToFleet(fleet, 11, 3);

        //Assert
        Assert.True(result);
        Assert.Single(fleet.Spaceships);
        
        Assert.Equal(1, planet.BattleUnits.Count(s => s.Quantity == 7));
    }
}