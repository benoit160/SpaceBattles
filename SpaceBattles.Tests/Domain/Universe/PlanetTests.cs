using SpaceBattles.Core.Domain.Entities.Universe;
using SpaceBattles.Core.Domain.Enums;

namespace SpaceBattles.Tests.Domain.Universe;

public class PlanetTests
{
    [Fact]
    public void PlanetConstructor()
    {
        // Arrange
        Planet planet = new Planet();
        
        // Assert
        Assert.False(string.IsNullOrWhiteSpace(planet.Name));
        Assert.NotEqual(default, planet.PlanetType);
        Assert.NotEqual(default, planet.OrbitalPeriod);
        Assert.NotEqual(default, planet.AverageSurfaceTemp);
        Assert.NotEqual(default, planet.Gravity);
        Assert.NotEqual(default, planet.LastUpdated);
        
        Assert.Equal(10, planet.Buildings.Count);
        Assert.Equal(150, planet.Titanium);
        Assert.Equal(75, planet.Silicon);
        Assert.Equal(0, planet.Helium);
    }
    
    [Fact]
    public void BaseResourceProduction()
    {
        // Arrange
        Planet planet = new();
        
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
        
        // Act
        planet.ResourcesUpdate(DateTime.Now + TimeSpan.FromDays(10));
        
        // Assert
        Assert.Equal(10_000, planet[Resource.Titanium]);
        Assert.Equal(10_000, planet[Resource.Silicon]);
        Assert.Equal(0, planet[Resource.Helium]);
    }
    
    [Fact]
    public void OutOfRangeIndexerGet()
    {
        // Arrange
        Planet planet = new Planet();

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            _ = planet[0];
        });
    }
    
    [Fact]
    public void OutOfRangeIndexerSet()
    {
        Planet planet = new Planet();

        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            const int outOfRangeIndex = 27;
            planet[(Resource)outOfRangeIndex] = 1;
        });
    }
    
    [Fact]
    public void OutOfRange_ResourceCapacity()
    {
        // Arrange
        Planet planet = new Planet();
        const int outOfRangeIndex = 27;

        // Act
        var result = planet.ResourceCapacity((Resource)outOfRangeIndex);
        
        // Assert
        Assert.Equal(0, result);
    }
    
    [Fact]
    public void OutOfRange_ResourceProduction()
    {
        // Arrange
        Planet planet = new Planet();
        const int outOfRangeIndex = 27;

        // Act
        var result = planet.ResourceProduction((Resource)outOfRangeIndex);
        
        // Assert
        Assert.Equal(0, result);
    }
    
    [Fact]
    public void ResourceUpdate()
    {
        // Arrange
        Planet planet = new Planet();
        DateTime advancedTime = DateTime.Now + TimeSpan.FromSeconds(5);
        
        // Act
        planet.ResourcesUpdate(advancedTime);
        
        // Assert
        Assert.Equal(152, planet[Resource.Titanium]);
        Assert.Equal(76, planet[Resource.Silicon]);
        Assert.Equal(0, planet[Resource.Helium]);
        
        advancedTime += TimeSpan.FromSeconds(5);
        
        planet.ResourcesUpdate(advancedTime);
    }
}