using System.Text.Json;
using Moq;
using SpaceBattles.Core.Application.Services;
using SpaceBattles.Core.Domain.Entities.Universe;
using SpaceBattles.Core.Domain.Enums;
using SpaceBattles.Core.Domain.Models;

namespace SpaceBattles.Tests.Application.Services;

public class SaveServiceTests
{
    private readonly Mock<IBrowserService> _browserService;
    private readonly GameState _gameState;

    public SaveServiceTests()
    {
        _browserService = new Mock<IBrowserService>();
        _gameState = new GameState();
        
        UniverseCreationModel model = new UniverseCreationModel
        {
            CommanderName = "Ben",
            StartingPlanetName = "Test",
            UniverseSize = UniverseSize.VeryLarge,
        };
        _gameState.Initialize(model);
        _gameState.CurrentPlanet.Init();
    }

    [Fact]
    public async Task SaveToStorage()
    {
        // Arrange
        SaveService service = new SaveService(_gameState, _browserService.Object);
        _browserService.Setup(bs => bs.WriteToLocalStorage(
                It.Is<string>(key => key == "SaveData"),
                It.IsAny<string>()));

        // Act
        Task action = service.SaveToStorage();
        await action;

        // Assert
        Assert.True(action.IsCompletedSuccessfully);
    }

    [Fact]
    public async Task LoadFromStorage()
    {
        // Arrange
        SaveService service = new SaveService(_gameState, _browserService.Object);

        string returnedJson = JsonSerializer.Serialize(_gameState.CurrentUniverse);
        
        _browserService.Setup(bs => bs.ReadLocalStorage(It.Is<string>(key => key == "SaveData")))
            .ReturnsAsync(returnedJson);

        Universe uni = _gameState.CurrentUniverse;
        
        // Act
        bool result = await service.LoadFromStorage();

        // Assert
        Universe uni2 = _gameState.CurrentUniverse;
        
        Assert.True(result);
        Assert.NotEqual(uni, uni2);
        Assert.Equal(uni.Planets.Length, uni2.Planets.Length);
        Assert.Equal(uni.Players.Count, uni2.Players.Count);

        for (int i = 0; i < uni.Planets.Length; i++)
        {
            Planet reference = uni.Planets[i];
            Planet toCompare = uni2.Planets[i];
            
            Assert.Equal(reference.GetHashCode(), toCompare.GetHashCode());
            
            Assert.Equal(reference.Titanium, toCompare.Titanium);
            Assert.Equal(reference.Silicon, toCompare.Silicon);
            Assert.Equal(reference.Helium, toCompare.Helium);
            
            Assert.Equal(reference.Buildings.Length, toCompare.Buildings.Length);
            Assert.Equal(reference.BattleUnits.Length, toCompare.BattleUnits.Length);
            Assert.Equal(reference.LastUpdated, toCompare.LastUpdated);
        }
    }
    
    [Fact]
    public async Task LoadFromStorage_InvalidKey_Or_NoData()
    {
        // Arrange
        SaveService service = new SaveService(_gameState, _browserService.Object);
        _browserService.Setup(bs => bs.ReadLocalStorage(It.IsAny<string>()))
            .ReturnsAsync(null as string);

        // Act
        bool result = await service.LoadFromStorage();

        // Assert
        Assert.False(result);
    }
}