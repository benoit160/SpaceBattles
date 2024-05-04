namespace SpaceBattles.Core.Application.Services;

using Microsoft.JSInterop;

public sealed class BrowserService : IBrowserService
{
    private readonly IJSRuntime _jsRuntime;

    public BrowserService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task<string?> ReadLocalStorage(string key)
    {
        return await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);
    }

    public async Task WriteToLocalStorage(string key, string value)
    {
        await _jsRuntime.InvokeAsync<string>("localStorage.setItem", key, value);
    }

    public async Task DeleteFromLocalStorage(string key)
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
    }

    public async Task SetBadge(int? number = null)
    {
        if (number is null)
        {
            await _jsRuntime.InvokeAsync<string>("navigator.setAppBadge");
            return;
        }

        await _jsRuntime.InvokeAsync<string>("navigator.setAppBadge", number);
    }
}