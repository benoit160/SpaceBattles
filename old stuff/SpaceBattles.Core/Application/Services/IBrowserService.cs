namespace SpaceBattles.Core.Application.Services;

public interface IBrowserService
{
    public string? ReadLocalStorage(string key);

    public void WriteToLocalStorage(string key, string value);

    public void SetBadge(int? number = null);
}