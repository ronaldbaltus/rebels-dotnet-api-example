using Rebels.ExampleProject.Data;
using Rebels.ExampleProject.Data.Entities;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;

namespace Rebels.ExampleProject.CQRS.Tests.Mocks;

public class UnitOfWorkMock : IUnitOfWork
{
    public DbSet<RebelEntity> Rebels { get; }

    public UnitOfWorkMock()
    {
        var rebels = new List<RebelEntity>();

        for (var i = 0; i < 1000; i++) rebels.Add(RandomRebelEntity());

        Rebels = rebels.AsQueryable().BuildMockDbSet().Object;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken) => SaveChangesAsync(true, cancellationToken);
    public Task<int> SaveChangesAsync(bool acceptAllChanges, CancellationToken cancellationToken) => Task.FromResult(1);

    private RebelEntity RandomRebelEntity()
    {
        var rnd = new Random();
        var vowels = "AEIOY".ToCharArray();
        var consonants = "BCDFGHJKLMNPQRSTVWXZ".ToCharArray();

        return new RebelEntity()
        {
            Id = Guid.NewGuid(),
            Name = string.Join(
                   "",
                   consonants[rnd.Next(consonants.Length - 1)], vowels[rnd.Next(vowels.Length - 1)],
                   consonants[rnd.Next(consonants.Length - 1)], vowels[rnd.Next(vowels.Length - 1)],
                   consonants[rnd.Next(consonants.Length - 1)], vowels[rnd.Next(vowels.Length - 1)],
                   consonants[rnd.Next(consonants.Length - 1)]
            )
        };
    }
}
