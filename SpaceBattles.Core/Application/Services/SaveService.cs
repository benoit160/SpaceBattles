using System.Text;
using System.Text.Json;
using SpaceBattles.Core.Domain.Entities.Battle;
using SpaceBattles.Core.Domain.Entities.Building;
using SpaceBattles.Core.Domain.Entities.Universe;

namespace SpaceBattles.Core.Application.Services;

public sealed class SaveService
{
    private readonly GameState _gameState;
    private readonly IBrowserService _browserService;

    private const string Key = "SaveData";
    
    public SaveService(GameState gameState, IBrowserService browserService)
    {
        _gameState = gameState;
        _browserService = browserService;
    }

    public async Task SaveToStorage()
    {
        JsonSerializerOptions options = new JsonSerializerOptions()
        {
            IgnoreReadOnlyProperties = true,
            IgnoreReadOnlyFields = true,
        };
        string json = JsonSerializer.Serialize(_gameState.CurrentUniverse, options);
        await _browserService.WriteToLocalStorage(Key, json);
    }

    public async Task<bool> LoadFromStorage()
    {
        string? data = await _browserService.ReadLocalStorage(Key);

        if (data is null) return false;
        
        Universe? universe = JsonSerializer.Deserialize<Universe>(data);
        
        if (universe is null) return false;

        RebuildEntityGraph(universe);
        _gameState.Restore(universe);
        return true;
    }

    private void RebuildEntityGraph(Universe universe)
    {
        var buildings = Building.Buildings();
        var spaceships = Spaceship.Spaceships();
        var defenses = Defense.Defenses();

        foreach (Planet planet in universe.Planets)
        {
            foreach (BuildingLevel buildingLevel in planet.Buildings)
            {
                buildingLevel.Building = buildings.Single(b => b.Id == buildingLevel.BuildingId);
            }
            
            foreach (CombatEntityInventory combatEntityInventory in planet.Defenses)
            {
                combatEntityInventory.CombatEntity = defenses.Single(defense => defense.Id == combatEntityInventory.CombatEntityId);
            }
            
            foreach (CombatEntityInventory combatEntityInventory in planet.Spaceships)
            {
                combatEntityInventory.CombatEntity = spaceships.Single(spaceship => spaceship.Id == combatEntityInventory.CombatEntityId);
            }
        }
    }
}