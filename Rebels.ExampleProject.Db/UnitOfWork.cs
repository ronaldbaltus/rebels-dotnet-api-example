using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rebels.ExampleProject.Db;
using Data;
using Rebels.ExampleProject.Data.Entities;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private DataContext _context;
    private bool disposedValue;

    public UnitOfWork(DataContext context)
    {
        _context = context;
        Rebels = new EntityRepository<RebelEntity>(
            _context.Rebels.AsQueryable(),
            async (r, ct) => (await _context.Rebels.AddAsync(r, ct)).Entity,
            (r, ct) => Task.FromResult(_context.Rebels.Update(r).Entity),
            (r, ct) => Task.FromResult(_context.Rebels.Remove(r).State == Microsoft.EntityFrameworkCore.EntityState.Deleted)
        );
    }

    public EntityRepository<RebelEntity> Rebels { get; private set; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => _context.SaveChangesAsync(cancellationToken);

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                _context.Dispose();
            }

            disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
