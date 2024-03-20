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
            if (planet.OwnerId is not null)
            {
                planet.Owner = universe.Players.Find(p => p.Id == planet.OwnerId);
            }

            for (int index = 0; index < planet.Buildings.Length; index++)
            {
                BuildingLevel buildingLevel = planet.Buildings[index];
                buildingLevel.Building = buildings.Single(b => b.Id == buildingLevel.BuildingId);
            }

            for (int index = 0; index < planet.Defenses.Length; index++)
            {
                CombatEntityInventory combatEntityInventory = planet.Defenses.Span[index];
                combatEntityInventory.CombatEntity = defenses.Single(defense => defense.Id == combatEntityInventory.CombatEntityId);
            }
            
            for (int index = 0; index < planet.Spaceships.Length; index++)
            {
                CombatEntityInventory combatEntityInventory = planet.Spaceships.Span[index];
                combatEntityInventory.CombatEntity = spaceships.Single(spaceship => spaceship.Id == combatEntityInventory.CombatEntityId);
            }
        }
    }
}