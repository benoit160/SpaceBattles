using MudBlazor;
using SpaceBattles.Core.Application.Services;

namespace SpaceBattles.Client.Services;

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