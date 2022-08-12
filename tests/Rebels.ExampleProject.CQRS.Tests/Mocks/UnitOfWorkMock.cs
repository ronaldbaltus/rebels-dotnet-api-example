using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rebels.ExampleProject.Data;
using Rebels.ExampleProject.Data.Entities;
using Moq;
using MockQueryable.Moq;

namespace Rebels.ExampleProject.CQRS.Tests.Mocks;

public class UnitOfWorkMock : IUnitOfWork
{
    private EntityRepository<RebelEntity> _Rebels;
    public Mock<Microsoft.EntityFrameworkCore.DbSet<RebelEntity>> RebelsSetMock;
    public EntityRepository<RebelEntity> Rebels => _Rebels;

    public UnitOfWorkMock()
    {
        var rebels = new List<RebelEntity>();

        for (var i = 0; i < 1000; i++) rebels.Add(RandomRebelEntity());

        RebelsSetMock = rebels.AsQueryable().BuildMockDbSet();

        _Rebels = new EntityRepository<RebelEntity>(
            RebelsSetMock.Object.AsQueryable(),
            (r, ct) => { rebels.Add(r); return Task.FromResult(r); }, 
            (r, ct) =>
            {
                var idx = rebels.FindIndex(re => re.Id == r.Id);
                if (idx == -1) return Task.FromResult<RebelEntity>(null!);
                rebels[idx] = r;
                return Task.FromResult(r);
            },
            (r, ct) =>
            {
                var idx = rebels.FindIndex(re => re.Id == r.Id);
                if (idx == -1) return Task.FromResult(false);
                rebels.RemoveAt(idx);
                return Task.FromResult(true);
            }
        );

    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(1);
    }

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
