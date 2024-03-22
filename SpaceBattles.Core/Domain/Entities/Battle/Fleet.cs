﻿namespace SpaceBattles.Core.Domain.Entities.Battle;

public class Fleet
{
    public required Player.Player Owner { get; init; }

    public short OwnerId { get; init; }

    public List<CombatEntityInventory> Spaceships { get; set; }
        = new List<CombatEntityInventory>();

    public int Fuel { get; set; }

    public FleetAction Action { get; set; }
}

public enum FleetAction
{
    Idle,
    Moving,
    Attacking,
    Transporting,
    Recycling,
    Spying,
}