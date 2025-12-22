namespace SpaceBattles.Core.Domain.Records;

using SpaceBattles.Core.Domain.Enums;

public record struct ResourceCost(Resource Resource, long RequiredQuantity);