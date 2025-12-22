using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SpaceBattles.Server.Infrastructure;

namespace SpaceBattles.E2E.Tests;

public sealed class SpaceBattlesInMemoryDbContext : SpaceBattlesDbContext
{
    public SpaceBattlesInMemoryDbContext(DbContextOptions<SpaceBattlesDbContext> options)
        : base(options)
    {
    }
    
    public SpaceBattlesInMemoryDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }

    public static SpaceBattlesInMemoryDbContext CreateContext()
    {
        SqliteConnection connection = new SqliteConnection("Filename=:memory:");
        connection.Open();

        DbContextOptions<SpaceBattlesInMemoryDbContext> options = new DbContextOptionsBuilder<SpaceBattlesInMemoryDbContext>()
            .UseSqlite(connection)
            .Options;

        SpaceBattlesInMemoryDbContext context = new(options);

        context.Database.EnsureCreated();

        return context;
    }
}