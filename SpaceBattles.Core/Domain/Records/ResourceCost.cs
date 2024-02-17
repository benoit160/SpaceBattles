using SpaceBattles.Core.Domain.Enums;

namespace SpaceBattles.Core.Domain.Records;

public record struct ResourceCost(Resource Resource, long RequiredQuantity);