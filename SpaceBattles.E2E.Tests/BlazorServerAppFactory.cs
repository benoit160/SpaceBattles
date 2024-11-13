using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;


namespace SpaceBattles.E2E.Tests;

public class BlazorServerAppFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            //  Inject new services or replace exsting ones here
            // ServiceDescriptor dbContext = services.Single(d => d.ServiceType == typeof(SpaceBattlesDbContext));
            // services.Remove(dbContext);
            // services.AddScoped<SpaceBattlesDbContext>();
        });
        
        builder.ConfigureAppConfiguration((_, config) =>
        {
            Dictionary<string, string> settings = new Dictionary<string, string>
            {
                ["CosmosDB:EndpointUrl"]  = "https://localhost:8081",
                ["CosmosDB:AccountKey"]  = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==",
                ["CosmosDB:DatabaseName"]  = "SpaceBattles.E2E",
            };
            config.AddInMemoryCollection(settings);
        });

        builder.UseEnvironment("Development");
    }
}