using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace SpaceBattles.Server.Infrastructure;

public class SpaceBattlesDbContext : DbContext
{
    public SpaceBattlesDbContext(DbContextOptions<SpaceBattlesDbContext> options)
        : base(options)
    {
    }
    
    protected SpaceBattlesDbContext(DbContextOptions options)
        : base(options)
    {
    }
    
    public DbSet<DayStatistics> Statistics { get; set; }
    
    public DbSet<SaveData> SaveData { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Get all types inheriting from CosmosEntity
        Type baseEntityType = typeof(CosmosEntity);
        IEnumerable<Type> entityTypes = modelBuilder.Model.GetEntityTypes()
            .Where(t => baseEntityType.IsAssignableFrom(t.ClrType) && t.ClrType != baseEntityType)
            .Select(t => t.ClrType);

        // Apply configuration to each entity type
        foreach (var entityType in entityTypes)
        {
            modelBuilder.Entity(entityType)
                .Property(nameof(CosmosEntity.Id))
                .ToJsonProperty("id");

            modelBuilder.Entity(entityType)
                .Property(nameof(CosmosEntity.PartitionKey))
                .ToJsonProperty("pk");
        }
        
        modelBuilder.Entity<SaveData>()
            .HasNoDiscriminator()
            .HasPartitionKey(x => x.PartitionKey)
            .HasQueryFilter(b => b.PartitionKey == CosmosEntity.ApplicationPartitionKey)
            .ToContainer(nameof(SaveData));
        
        modelBuilder.Entity<DayStatistics>()
            .HasNoDiscriminator()
            .HasPartitionKey(x => x.PartitionKey)
            .HasQueryFilter(b => b.PartitionKey == CosmosEntity.ApplicationPartitionKey)
            .ToContainer(nameof(Statistics));
    }
}

public abstract class CosmosEntity
{
    public static readonly string ApplicationPartitionKey = "SpaceBattles"; 
    
    public Guid Id { get; set; }

    public string PartitionKey { get; set; }
        = ApplicationPartitionKey;
}

public sealed class DayStatistics : CosmosEntity
{
    public DayStatistics()
    {
        Id = Guid.NewGuid();
        Date = DateOnly.FromDateTime(DateTime.Now);
    }
    
    public DateOnly Date { get; init; }

    public int Logins { get; set; }
}

public sealed class SaveData : CosmosEntity
{
    [SetsRequiredMembers]
    public SaveData(string data)
    {
        Id = Guid.NewGuid();
        Data = data;
        CreatedAt = DateTime.Now;
        LastSave = DateTime.Now;
    }
    
    public required string Data { get; set; }

    public DateTime CreatedAt { get; init; }
    
    public DateTime LastSave { get; set; }

    public void Update(string newSaveData)
    {
        Data = newSaveData;
        LastSave = DateTime.Now;
    }
}