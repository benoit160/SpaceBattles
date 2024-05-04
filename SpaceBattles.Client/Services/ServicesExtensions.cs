using SpaceBattles.Core.Application.Services;

namespace SpaceBattles.Client.Services;

public static class ServicesExtensions
{
    public static void AddSpaceBattlesServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<INotificationService, NotificationService>();
        serviceCollection.AddScoped<BrowserService>();
    }
}