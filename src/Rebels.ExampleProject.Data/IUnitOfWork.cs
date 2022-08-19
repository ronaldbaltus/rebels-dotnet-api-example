namespace Rebels.ExampleProject.Data;
using System.Threading.Tasks;
using Entities;

public interface IUnitOfWork
{
    public EntityRepository<RebelEntity> Rebels { get; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

