using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using SpaceBattles.Core.Application.Services;
using SpaceBattles.Core.Domain.Entities.Universe;

namespace SpaceBattles.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<GameState>();
            builder.Services.AddScoped<PlanetService>();
            builder.Services.AddScoped<StatisticService>();
            builder.Services.AddMudServices();

            await builder.Build().RunAsync();
        }
    }
}
