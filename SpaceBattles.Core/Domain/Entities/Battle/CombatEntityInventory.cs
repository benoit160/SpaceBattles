using System.Text.Json.Serialization;

namespace SpaceBattles.Core.Domain.Entities.Battle;

#nullable disable annotations
public class CombatEntityInventory
{
    [JsonIgnore]
    public CombatEntity CombatEntity { get; init; }
    
    public short CombatEntityId { get; init; }
    
    public int Quantity { get; set; }
}