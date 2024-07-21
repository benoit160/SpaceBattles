using SpaceBattles.Core.Application.Services;
using SpaceBattles.UI.Services;

namespace SpaceBattles.Client.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static void AddCoreSpaceBattlesServices(this IServiceCollection services)
    {
        services.AddScoped<GameState>();
        services.AddScoped<PlanetService>();
        services.AddScoped<StatisticService>();
        services.AddScoped<SaveService>();
        services.AddScoped<BotService>();
        services.AddScoped<ITimeProvider, Core.Application.Services.TimeProvider>(_ => new Core.Application.Services.TimeProvider(secondsInterval: 1));
        services.AddScoped<IBrowserService, BrowserService>();
    }
}