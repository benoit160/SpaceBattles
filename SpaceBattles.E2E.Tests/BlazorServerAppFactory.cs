using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace SpaceBattles.E2E.Tests;

public class BlazorServerAppFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(_ =>
        {
            Environment.SetEnvironmentVariable("CosmosDB__EndpointUrl", "localhost:8081");
            Environment.SetEnvironmentVariable("CosmosDB__AccountKey", "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==");
            Environment.SetEnvironmentVariable("CosmosDB__DatabaseName", "SpaceBattles");
        });

        builder.UseEnvironment("Development");
    }
}