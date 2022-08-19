using Microsoft.EntityFrameworkCore;
using Rebels.ExampleProject.Data.Entities;

namespace Rebels.ExampleProject.Data;

public class UnitOfWork : DbContext, IUnitOfWork
{
    public DbSet<RebelEntity> Rebels { get; set; } = null!;

    public UnitOfWork() : base() { }
    public UnitOfWork(DbContextOptions options) : base(options) { }

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
