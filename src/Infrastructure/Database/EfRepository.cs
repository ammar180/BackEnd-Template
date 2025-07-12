using Microsoft.EntityFrameworkCore;
using SharedKernel;
using System.Linq.Expressions;

namespace Infrastructure.Database;
public class EfRepository<T>(ApplicationDbContext dbContext)
    : IReadRepository<T>, IRepository<T> where T : class
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    public IQueryable<T> Query => _dbContext.Set<T>();

    public async Task<T> Add(T entity, CancellationToken cancellation = default)
    {
        _dbContext.Set<T>().Add(entity);
        await _dbContext.SaveChangesAsync(cancellation);
        return entity;
    }

    public async Task Delete(T entity, CancellationToken cancellation = default)
    {
        _dbContext.Set<T>().Remove(entity);
        await _dbContext.SaveChangesAsync(cancellation);
    }

    public async Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancellation = default)
    {
        return await _dbContext.Set<T>().FindAsync([id], cancellationToken: cancellation);
    }

    public async Task<T?> GetFirstOrDefaultAsync(IQueryable<T> query, string[]? includes = null, CancellationToken cancellation = default)
    {
        if (includes != null)
        {
            foreach (string include in includes)
            {
                query = query.Include(include);
            }
        }

        return await query.FirstOrDefaultAsync(cancellation);
    }

    public async Task<List<T>> GetListAsync(IQueryable<T> query, string[]? includes = null, CancellationToken cancellation = default)
    {
        if (includes != null)
        {
            foreach (string include in includes)
            {
                query = query.Include(include);
            }
        }

        return await query.ToListAsync(cancellation);
    }

    public async Task Update(T entity, CancellationToken cancellation = default)
    {
        _dbContext.Set<T>().Update(entity);
        await _dbContext.SaveChangesAsync(cancellation);
    }
    public async Task<TResult?> GetFirstOrDefaultAsync<TResult>(
    IQueryable<T> query,
    Expression<Func<T, TResult>> selector,
    string[]? includes = null,
    CancellationToken cancellation = default)
    {
        if (includes != null)
        {
            foreach (string include in includes)
            {
                query = query.Include(include);
            }
        }

        return await query.Select(selector).FirstOrDefaultAsync(cancellation);
    }

    public async Task<List<TResult>> GetListAsync<TResult>(
        IQueryable<T> query,
        Expression<Func<T, TResult>> selector,
        string[]? includes = null,
        CancellationToken cancellation = default)
    {
        if (includes != null)
        {
            foreach (string include in includes)
            {
                query = query.Include(include);
            }
        }

        return await query.Select(selector).ToListAsync(cancellation);
    }

}
