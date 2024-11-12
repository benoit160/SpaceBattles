using Microsoft.Playwright;
using NUnit.Framework;

namespace SpaceBattles.E2E.Tests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Tests : BlazorTest
{
    // dotnet test -- Playwright.LaunchOptions.Channel=chrome Playwright.LaunchOptions.Headless=false
    // dotnet test -s .\spaceBattles.E2E.Tests\local.runsettings

    [Test]
    public async Task HomePage()
    {
        List<IRequest> requests = [];
        
        Page.Request += (_, request) => requests.Add(request);
        
        await Page.GotoAsync(RootUri.AbsoluteUri);

        // Expect a title "to contain" a substring.
        await Expect(Page).ToHaveTitleAsync("Welcome to SpaceBattles");

        Assert.IsNotEmpty(requests.Where(r => r.Url.Contains("/api/telemetry") && r.Method == "POST" && r.ResponseAsync().Result.Ok));
    }
    
    [Test]
    public async Task Play()
    {
        List<IRequest> requests = [];
        
        Page.Request += (_, request) => requests.Add(request);
        
        await Page.GotoAsync(RootUri.AbsoluteUri);
        
        await Page.GetByRole(AriaRole.Button, new() { NameString = "Play" }).ClickAsync();
        
        // Expect a title "to contain" a substring.
        await Expect(Page).ToHaveTitleAsync("SpaceBattles");

        Assert.IsNotEmpty(requests.Where(r => r.Url.Contains("/api/telemetry") && r.Method == "POST"));
    }
    
    [TestCase("overview")]
    [TestCase("buildings")]
    [TestCase("shipyard")]
    [TestCase("defenses")]
    [TestCase("universe")]
    [TestCase("statistics")]
    public async Task PageNavigation(string buttonName)
    {
        List<IRequest> requests = [];
        
        Page.Request += (_, request) => requests.Add(request);
        
        await Page.GotoAsync(RootUri.AbsoluteUri);
        
        await Page.GetByRole(AriaRole.Button, new() { NameString = "play" }).ClickAsync();
        
        await Page.GetByRole(AriaRole.Link, new() { NameString = buttonName }).ClickAsync();
        
        // Expect a title "to contain" a substring.
        await Expect(Page).ToHaveTitleAsync("SpaceBattles");

        Assert.IsNotEmpty(requests.Where(r => r.Url.Contains("/api/telemetry") && r.Method == "POST"));
    }
}