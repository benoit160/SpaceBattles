namespace SpaceBattles.Core.Application.Services;

public interface IBrowserService
{
    public Task<string?> ReadLocalStorage(string key);

    public Task WriteToLocalStorage(string key, string value);

    public Task DeleteFromLocalStorage(string key);

    public Task SetBadge(int? number = null);
}