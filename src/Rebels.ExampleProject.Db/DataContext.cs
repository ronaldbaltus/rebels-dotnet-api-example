namespace Rebels.ExampleProject.Db;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

public class DataContext : DbContext
{
    public DbSet<RebelEntity> Rebels { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured) return;

        // Use memory if nothing else is defined
        optionsBuilder.UseInMemoryDatabase("ExampleProject");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder
            .Entity<RebelEntity>()
            .HasData(
                new RebelEntity() { Name = "Ezra Bridger" },
                new RebelEntity() { Name = "Kanan Jarrus" },
                new RebelEntity() { Name = "Hera Syndulla" },
                new RebelEntity() { Name = "Sabine Wren" },
                new RebelEntity() { Name = "Garazeb \"Zeb\" Orrelios" },
                new RebelEntity() { Name = "Chopper" }
            );
    }
}
