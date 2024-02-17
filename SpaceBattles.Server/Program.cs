using Microsoft.AspNetCore.StaticFiles;

namespace SpaceBattles.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
            provider.Mappings.Add(".avif", "image/avif");
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