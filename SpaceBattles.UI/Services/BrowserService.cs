using Microsoft.JSInterop;
using SpaceBattles.Core.Application.Services;

namespace SpaceBattles.UI.Services;

public sealed class BrowserService : IBrowserService
{
    private readonly IJSInProcessRuntime _jsRuntime;

    public BrowserService(IJSRuntime jsRuntime)
    {
        _jsRuntime = (jsRuntime as IJSInProcessRuntime)
                     ?? throw new Exception("Could not convert IJSRuntime to IJSInProcessRuntime");
    }

    public string? ReadLocalStorage(string key)
    {
        return _jsRuntime.Invoke<string?>("localStorage.getItem", key);
    }

    public void WriteToLocalStorage(string key, string value)
    {
        _jsRuntime.Invoke<string>("localStorage.setItem", key, value);
    }

    public void SetBadge(int? number = null)
    {
        if (number is null)
        {
            _jsRuntime.Invoke<string>("navigator.setAppBadge");
            return;
        }

        _jsRuntime.Invoke<string>("navigator.setAppBadge", number);
    }
}