using MudBlazor;
using SpaceBattles.Core.Application.Services;

namespace SpaceBattles.Client.Services;

public static class ServicesExtensions
{
    public static void AddSpaceBattlesServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<GameState>();
        serviceCollection.AddScoped<PlanetService>();
        serviceCollection.AddScoped<StatisticService>();
        serviceCollection.AddScoped<INotificationService, NotificationService>();
    }
}

public sealed class NotificationService  : INotificationService
{
    private readonly ISnackbar _snackbar;

    public NotificationService(ISnackbar snackbar)
    {
        _snackbar = snackbar;
    }

    public void NotifyInfo(string text)
    {
        _snackbar.Add(text, Severity.Info);
    }
}