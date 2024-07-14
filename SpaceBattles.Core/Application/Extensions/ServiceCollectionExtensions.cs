namespace SpaceBattles.Core.Application.Extensions;

using Microsoft.Extensions.DependencyInjection;
using SpaceBattles.Core.Application.Services;

public static class ServiceCollectionExtensions
{
    public static void AddCoreSpaceBattlesServices(this IServiceCollection services)
    {
        services.AddScoped<GameState>();
        services.AddScoped<PlanetService>();
        services.AddScoped<StatisticService>();
        services.AddScoped<SaveService>();
        services.AddScoped<BotService>();
        services.AddScoped<IBrowserService, BrowserService>();
    }
}