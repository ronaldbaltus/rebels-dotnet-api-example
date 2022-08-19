namespace Rebels.ExampleProject.Data;
using System.Threading.Tasks;
using Entities;
using Microsoft.EntityFrameworkCore;

public interface IUnitOfWork
{
    public DbSet<RebelEntity> Rebels { get; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    public Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
}

