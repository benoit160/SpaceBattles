using SpaceBattles.Core.Domain.Enums;

namespace SpaceBattles.Core.Domain.ValueTypes;

public record struct ResourceCost(Resource Resource, long RequiredQuantity);