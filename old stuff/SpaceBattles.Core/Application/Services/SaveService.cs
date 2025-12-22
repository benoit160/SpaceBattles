namespace SpaceBattles.Core.Application.Services;

using System.Text.Json;
using SpaceBattles.Core.Domain.Entities.Battle;
using SpaceBattles.Core.Domain.Entities.Building;
using SpaceBattles.Core.Domain.Entities.Universe;

public sealed class SaveService
{
    private const string Key = "SaveData";

    private readonly GameState _gameState;
    private readonly StatisticService _statistics;
    private readonly IBrowserService _browserService;

    private readonly JsonSerializerOptions _options;

    public SaveService(GameState gameState, StatisticService statisticService, IBrowserService browserService)
    {
        _gameState = gameState;
        _statistics = statisticService;
        _browserService = browserService;
        _options = new JsonSerializerOptions
        {
            IgnoreReadOnlyProperties = true,
            IgnoreReadOnlyFields = true,
        };
    }

    public void SaveToStorage()
    {
        string game = JsonSerializer.Serialize(_gameState.CurrentUniverse, _options);

        _browserService.WriteToLocalStorage(Key, game);
    }

    public string? GetLoadData()
        => _browserService.ReadLocalStorage(Key);

    public bool LoadFromStorage(string data)
    {
        Universe? universe;

        try
        {
            universe = JsonSerializer.Deserialize<Universe>(data);
        }
        catch
        {
            return false;
        }

        if (universe is null) return false;

        RebuildEntityGraph(universe);
        _gameState.SetState(universe);
        _statistics[_gameState.CurrentPlanet.Id] = universe.Statistics;

        return true;
    }

    private void RebuildEntityGraph(Universe universe)
    {
        var buildings = Building.Buildings();
        var spaceships = Spaceship.Spaceships();
        var defenses = Defense.Defenses();

        foreach (Planet planet in universe.Planets)
        {
            if (planet.LastUpdated == default) continue;

            if (planet.OwnerId is not null)
            {
                planet.Owner = universe.Players.Find(p => p.Id == planet.OwnerId);
            }

            for (int index = 0; index < planet.Buildings.Length; index++)
            {
                BuildingLevel buildingLevel = planet.Buildings[index];
                buildingLevel.Building = buildings.Single(b => b.Id == buildingLevel.BuildingId);
            }

            for (int index = 0; index < planet.BattleUnits.Length; index++)
            {
                CombatEntityInventory entityInventory = planet.BattleUnits[index];
                CombatEntity? match = Array.Find<CombatEntity>(spaceships, b => b.Id == entityInventory.CombatEntityId)
                                      ?? Array.Find<CombatEntity>(defenses, b => b.Id == entityInventory.CombatEntityId);

                entityInventory.CombatEntity = match;
            }

            planet.Spaceships = planet.BattleUnits.AsMemory(8, 10);
            planet.Defenses = planet.BattleUnits.AsMemory(0, 8);
        }
    }
}