namespace SpaceBattles.Core.Domain.Entities.Battle;

using System.Text.Json.Serialization;

#nullable disable annotations
public class CombatEntityInventory
{
    [JsonIgnore]
    public CombatEntity CombatEntity { get; set; }

    public short CombatEntityId { get; init; }

    public int Quantity { get; set; }
}