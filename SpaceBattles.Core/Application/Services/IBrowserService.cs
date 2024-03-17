namespace SpaceBattles.Core.Application.Services;

public interface IBrowserService
{
    Task<string?> ReadLocalStorage(string key);

    Task WriteToLocalStorage(string key, string value);

    Task SetBadge(int? number = null);
}