using System.Linq.Expressions;

namespace SharedKernel;
public interface IReadRepository<T> where T : class
{
    IQueryable<T> Query { get; } // base query
    Task<List<T>> GetListAsync(IQueryable<T>? query = null, string[]? includes = null, CancellationToken cancellation = default);
    Task<T?> GetFirstOrDefaultAsync(IQueryable<T>? query = null, string[]? includes = null, CancellationToken cancellation = default);
    Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellation = default); // lazy loading

    Task<List<TResult>> GetListAsync<TResult>(
        IQueryable<T> query,
        Expression<Func<T, TResult>> selector,
        string[]? includes = null,
        CancellationToken cancellation = default);
    Task<TResult?> GetFirstOrDefaultAsync<TResult>(
    IQueryable<T> query,
    Expression<Func<T, TResult>> selector,
    string[]? includes = null,
    CancellationToken cancellation = default);
}
public interface IRepository<T> : IReadRepository<T> where T : class
{
    Task<T> Add(T entity, CancellationToken cancellation = default);
    Task Update(T entity, CancellationToken cancellation = default);
    Task Delete(T entity, CancellationToken cancellation = default);
}
