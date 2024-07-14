using SpaceBattles.Core.Domain.Entities.Universe;

namespace SpaceBattles.Tests.Domain.Upgrade;

public class ShipyardConstructionTests
{
    [Fact]
    public void TryConstructShipyard_BuildingRequirements_NotMet()
    {
        // Arrange
        Planet planet = new Planet();
        planet.Init();

        //  Act
        bool result = planet.TryConstructShipyard(1, 1);

        // Assert
        Assert.False(result);
        Assert.Null(planet.ShipyardConstruction);
    }
    
    [Fact]
    public void TryConstructShipyard_NotEnoughResources()
    {
        // Arrange
        Planet planet = new Planet();
        planet.Init();
        Array.ForEach(planet.Buildings, b => b.Level = 10);

        //  Act
        bool result = planet.TryConstructShipyard(1, 1);

        // Assert
        Assert.False(result);
        Assert.Null(planet.ShipyardConstruction);
    }

    [Fact]
    public void TryConstructShipyard_ShipyardAlreadyUsed()
    {
        // Arrange
        Planet planet = new Planet();
        planet.Init();
        Array.ForEach(planet.Buildings, b => b.Level = 10);
        planet.LastUpdated -= TimeSpan.FromDays(1);
        planet.ResourcesUpdate(DateTime.Now, stackalloc long[3]);
        planet.TryConstructShipyard(1, 1);

        //  Act
        bool result = planet.TryConstructShipyard(1, 1);

        // Assert
        Assert.False(result);
        Assert.NotNull(planet.ShipyardConstruction);
    }

    [Fact]
    public void TryConstructShipyard_Success()
    {
        // Arrange
        Planet planet = new Planet();
        planet.Init();
        Array.ForEach(planet.Buildings, b => b.Level = 1);
        planet.LastUpdated -= TimeSpan.FromDays(1);
        planet.ResourcesUpdate(DateTime.Now, stackalloc long[3]);
        long titaniumBefore = planet.Titanium;

        //  Act
        bool result = planet.TryConstructShipyard(1, 1);

        // Assert
        Assert.True(result); 
        Assert.NotNull(planet.ShipyardConstruction);
        Assert.Equal(1, planet.ShipyardConstruction.Quantity);
        Assert.Equal(1, planet.ShipyardConstruction.CombatEntityId);
        Assert.Equal(0.4d, planet.ShipyardConstruction.Duration.TotalHours);
        Assert.Equal(titaniumBefore - 2000, planet.Titanium);
    }
    
    [Fact]
    public void TryConstructShipyard_Success_MultipleQuantity()
    {
        // Arrange
        Planet planet = new Planet();
        planet.Init();
        Array.ForEach(planet.Buildings, b => b.Level = 10);
        planet.LastUpdated -= TimeSpan.FromDays(1);
        planet.ResourcesUpdate(DateTime.Now, stackalloc long[3]);
        long titaniumBefore = planet.Titanium;

        //  Act
        bool result = planet.TryConstructShipyard(1, 10);

        // Assert
        double expected = 20_000 / (2500d * 11);
        Assert.True(result);
        Assert.NotNull(planet.ShipyardConstruction);
        Assert.Equal(10, planet.ShipyardConstruction.Quantity);
        Assert.Equal(1, planet.ShipyardConstruction.CombatEntityId);
        Assert.Equal(expected, planet.ShipyardConstruction.Duration.TotalHours, 5);
        Assert.Equal(titaniumBefore - 20000, planet.Titanium);
    }

    [Fact]
    public void ProcessShipyard_NoConstruction()
    {
        // Arrange
        Planet planet = new Planet();
        planet.Init();

        // Act
        bool result = planet.ProcessShipyard(DateTime.Now);

        // Assert
        Assert.False(result);
        Assert.All(planet.BattleUnits, bu => bu.Quantity = 0);
    }
    
    [Fact]
    public void ProcessShipyard_ConstructionNotFinished()
    {
        // Arrange
        Planet planet = new Planet();
        planet.Init();
        Array.ForEach(planet.Buildings, b => b.Level = 10);
        planet.LastUpdated -= TimeSpan.FromDays(1);
        planet.ResourcesUpdate(DateTime.Now, stackalloc long[3]);
        planet.TryConstructShipyard(1, 10);

        // Act
        bool result = planet.ProcessShipyard(DateTime.Now + TimeSpan.FromMinutes(20));

        // Assert
        Assert.False(result);
    }
    
    [Fact]
    public void ProcessShipyard_Success()
    {
        // Arrange
        Planet planet = new Planet();
        planet.Init();
        Array.ForEach(planet.Buildings, b => b.Level = 10);
        planet.LastUpdated -= TimeSpan.FromDays(1);
        planet.ResourcesUpdate(DateTime.Now, stackalloc long[3]);
        planet.TryConstructShipyard(1, 10);

        // Act
        bool result = planet.ProcessShipyard(DateTime.Now + TimeSpan.FromDays(1));

        // Assert
        Assert.True(result);
        Assert.Equal(10,  planet.Defenses.Span[0].Quantity);
    }
}