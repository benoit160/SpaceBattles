using System.Net.Mime;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using SpaceBattles.Server.Infrastructure;

namespace SpaceBattles.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            
            builder.Configuration.AddJsonFile(
                "appsettings.secrets.json",
                optional: true,
                reloadOnChange: true);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            
            builder.Services.AddDbContext<SpaceBattlesDbContext>(options =>
            {
                CosmosDbSettings cosmosDbConfig = builder.Configuration.GetSection("CosmosDB").Get<CosmosDbSettings>()
                                                  ?? throw new ApplicationException("Missing CosmosDb Settings");
                options.UseCosmos(
                    accountEndpoint: cosmosDbConfig.EndpointUrl,
                    accountKey: cosmosDbConfig.AccountKey,
                    databaseName: cosmosDbConfig.DatabaseName);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseBlazorFrameworkFiles();

            FileExtensionContentTypeProvider provider = new FileExtensionContentTypeProvider();
            provider.Mappings.Add(".avif", MediaTypeNames.Image.Avif);
            app.UseStaticFiles(new StaticFileOptions()
            {
                ContentTypeProvider = provider,
            });
            
            app.UseAuthorization();
            
            app.MapControllers();
            app.MapFallbackToFile("index.html");

            app.Run();
        }
    }
}