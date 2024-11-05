using Microsoft.Playwright;
using NUnit.Framework;

namespace SpaceBattles.E2E.Tests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Tests : BlazorTest
{
    // dotnet test -- Playwright.LaunchOptions.Channel=chrome Playwright.LaunchOptions.Headless=false
    
    [Test]
    public async Task HomePage()
    {
        List<IRequest> requests = [];
        
        Page.Request += (_, request) => requests.Add(request);
        
        await Page.GotoAsync(RootUri.AbsoluteUri);

        // Expect a title "to contain" a substring.
        await Expect(Page).ToHaveTitleAsync("Welcome to SpaceBattles");

        Assert.IsNotEmpty(requests.Where(r => r.Url.Contains("/api/telemetry") && r.Method == "POST"));

        await Page.PauseAsync();
    }
}