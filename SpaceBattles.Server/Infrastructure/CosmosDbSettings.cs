namespace SpaceBattles.Server.Infrastructure;

public sealed class CosmosDbSettings
{
    public required string EndpointUrl { get; set; }

    public required string AccountKey { get; set; }

    public required string DatabaseName { get; set; }
}