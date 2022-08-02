namespace Rebels.ExampleProject.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
public class EntityRepository<T> : IQueryable<T>
{
    private IQueryable<T> _queryable;
    private Func<T, CancellationToken, Task<T>> _create;
    private Func<T, CancellationToken, Task<T>> _update;
    private Func<T, CancellationToken, Task<bool>> _delete;

    public EntityRepository(IQueryable<T> queryable, Func<T, CancellationToken, Task<T>> create, Func<T, CancellationToken, Task<T>> update, Func<T, CancellationToken, Task<bool>> delete)
    {
        _queryable = queryable;
        _create = create;
        _update = update;
        _delete = delete;
    }
    public Type ElementType => _queryable.ElementType;
    public Expression Expression => _queryable.Expression;
    public IQueryProvider Provider => _queryable.Provider;

    public IEnumerator<T> GetEnumerator() => _queryable.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => _queryable.GetEnumerator();

    public Task<T> CreateAsync(T entity, CancellationToken cancellationToken) => _create(entity, cancellationToken);
    public Task<T> Update(T entity, CancellationToken cancellationToken) => _update(entity, cancellationToken);
    public Task<bool> Delete(T entity, CancellationToken cancellationToken) => _delete(entity, cancellationToken);
}
