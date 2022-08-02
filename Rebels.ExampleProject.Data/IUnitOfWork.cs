namespace Rebels.ExampleProject.Data;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Entities;

public interface IUnitOfWork
{
    public EntityRepository<RebelEntity> Rebels { get; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

