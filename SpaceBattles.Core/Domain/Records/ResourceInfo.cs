using SpaceBattles.Core.Domain.Enums;

namespace SpaceBattles.Core.Domain.Records;

public record struct ResourceInfo(Resource Resource, string Name, string ImagePath);